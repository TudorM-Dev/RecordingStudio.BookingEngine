using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;

namespace RecordingStudio.BookingEngine.Core.Services;

public class ClosureService
{
    private readonly IStudioClosureRepository _closureRepository;
    private readonly IBookingRepository _bookingRepository;

    public ClosureService(
        IStudioClosureRepository closureRepository,
        IBookingRepository bookingRepository)
    {
        _closureRepository = closureRepository;
        _bookingRepository = bookingRepository;
    }

    // Rule 6: registering a closure auto-cancels any confirmed bookings it overlaps
    public async Task<(StudioClosure Closure, IReadOnlyList<Booking> CancelledBookings)> CreateClosureAsync(
        int studioId, DateTime startDateTime, DateTime endDateTime, string? reason)
    {
        StudioClosure closure = new()
        {
            StudioId = studioId,
            StartDateTime = startDateTime,
            EndDateTime = endDateTime,
            Reason = reason
        };

        await _closureRepository.AddAsync(closure);

        IReadOnlyList<Booking> confirmed = await _bookingRepository.GetConfirmedBookingsForStudioAsync(studioId);
        List<Booking> overlapping = confirmed
            .Where(b => b.StartDateTime < endDateTime && startDateTime < b.StartDateTime.AddHours(b.DurationHours))
            .ToList();

        if (overlapping.Count > 0)
        {
            await _bookingRepository.CancelAsync(overlapping);
        }

        return (closure, overlapping);
    }
}
