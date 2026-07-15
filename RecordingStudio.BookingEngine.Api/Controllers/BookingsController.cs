using Microsoft.AspNetCore.Mvc;
using RecordingStudio.BookingEngine.Api.Contracts;
using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Services;

namespace RecordingStudio.BookingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly BookingService _bookingService;

    public BookingsController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate(BookingRequest request)
    {
        ValidationResult result = await _bookingService.ValidateBookingAsync(
            request.StudioId,
            request.StartDateTime,
            request.DurationHours);

        if (!result.IsValid)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(new { message = "The booking is valid." });
    }
}