using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RecordingStudio.BookingEngine.Api.Contracts;
using RecordingStudio.BookingEngine.Api.Hubs;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Services;

namespace RecordingStudio.BookingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClosuresController : ControllerBase
{
    private readonly ClosureService _closureService;
    private readonly IHubContext<BookingHub> _hub;

    public ClosuresController(ClosureService closureService, IHubContext<BookingHub> hub)
    {
        _closureService = closureService;
        _hub = hub;
    }

    // Registering a closure auto-cancels any confirmed bookings it overlaps (rule 6)
    [HttpPost]
    public async Task<IActionResult> Create(ClosureRequest request)
    {
        if (request.EndDateTime <= request.StartDateTime)
        {
            return BadRequest(new { error = "End time must be after start time." });
        }

        (StudioClosure closure, IReadOnlyList<Booking> cancelled) = await _closureService.CreateClosureAsync(
            request.StudioId, request.StartDateTime, request.EndDateTime, request.Reason);

        if (cancelled.Count > 0)
        {
            await _hub.Clients.All.SendAsync(BookingHub.BookingsChanged, request.StudioId);
        }

        return Ok(new
        {
            closure.Id,
            closure.StudioId,
            closure.StartDateTime,
            closure.EndDateTime,
            cancelledBookingIds = cancelled.Select(b => b.Id)
        });
    }
}
