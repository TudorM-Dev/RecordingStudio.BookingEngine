using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;

namespace RecordingStudio.BookingEngine.Core.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly BookingValidator _validator;

    public BookingService(IBookingRepository bookingRepository, BookingValidator validator)
    {
        _bookingRepository = bookingRepository;
        _validator = validator;
    }

    public async Task<ValidationResult> ValidateBookingAsync(int studioId, DateTime startDateTime, int durationHours)
    {
        // Cheap, DB-free rules first
        ValidationResult scheduleResult = _validator.ValidateScheduleResult(startDateTime, durationHours);
        if (!scheduleResult.IsValid)
        {
            return scheduleResult;
        }

        // Data-dependent rule: buffer/overlap against existing bookings
        IReadOnlyList<Booking> existing =
            await _bookingRepository.GetConfirmedBookingsForStudioAsync(studioId);

        return _validator.ValidateNoOverlap(startDateTime, durationHours, existing);
    }
}