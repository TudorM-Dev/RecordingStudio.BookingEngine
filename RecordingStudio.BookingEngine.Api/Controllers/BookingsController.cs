using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecordingStudio.BookingEngine.Api.Contracts;
using RecordingStudio.BookingEngine.Api.Hubs;
using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Services;

namespace RecordingStudio.BookingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;
    private readonly IHubContext<BookingHub> _hub;

    public BookingsController(BookingService bookingService, IHubContext<BookingHub> hub)
    {
        _bookingService = bookingService;
        _hub = hub;
    }

    [HttpGet("studio/{studioId:int}")]
    public async Task<IActionResult> GetForStudio(int studioId)
    {
        IReadOnlyList<Booking> bookings = await _bookingService.GetBookingsForStudioAsync(studioId);
        return Ok(bookings.Select(ToResponse));
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate(BookingRequest request)
    {
        ValidationResult result = await _bookingService.ValidateBookingAsync(
            request.StudioId, request.ServiceTypeId, request.StartDateTime, request.DurationHours);

        if (!result.IsValid)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(new { message = "The booking is valid." });
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookingRequest request)
    {
        (ValidationResult validation, Booking? booking) = await _bookingService.CreateBookingAsync(
            request.StudioId, request.UserId, request.ServiceTypeId, request.StartDateTime, request.DurationHours);

        if (!validation.IsValid || booking is null)
        {
            return BadRequest(new { error = validation.Error });
        }

        await _hub.Clients.All.SendAsync(BookingHub.BookingsChanged, booking.StudioId);
        return Ok(ToResponse(booking));
    }

    private static object ToResponse(Booking booking) => new
    {
        booking.Id,
        booking.StudioId,
        booking.UserId,
        booking.ServiceTypeId,
        booking.StartDateTime,
        booking.DurationHours,
        Status = booking.Status.ToString()
    };
}
