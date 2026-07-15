using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Enums;
using RecordingStudio.BookingEngine.Core.Interfaces;

namespace RecordingStudio.BookingEngine.Core.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IStudioRepository _studioRepository;
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly IStudioClosureRepository _closureRepository;
    private readonly BookingValidator _validator;

    public BookingService(
        IBookingRepository bookingRepository,
        IStudioRepository studioRepository,
        IServiceTypeRepository serviceTypeRepository,
        IStudioClosureRepository closureRepository,
        BookingValidator validator)
    {
        _bookingRepository = bookingRepository;
        _studioRepository = studioRepository;
        _serviceTypeRepository = serviceTypeRepository;
        _closureRepository = closureRepository;
        _validator = validator;
    }

    // Runs every booking rule, cheapest first, stopping at the first failure
    public async Task<ValidationResult> ValidateBookingAsync(
        int studioId, int serviceTypeId, DateTime startDateTime, int durationHours)
    {
        // Rules 1 & 2 — pure, no database
        ValidationResult scheduleResult = _validator.ValidateScheduleResult(startDateTime, durationHours);
        if (!scheduleResult.IsValid) return scheduleResult;

        // Rule 4 — service offered by this studio
        IReadOnlyList<int> studioFacilityIds = await _studioRepository.GetFacilityIdsAsync(studioId);
        IReadOnlyList<int> requiredFacilityIds = await _serviceTypeRepository.GetRequiredFacilityIdsAsync(serviceTypeId);
        bool isExcluded = await _studioRepository.IsServiceExcludedAsync(studioId, serviceTypeId);

        ValidationResult serviceResult = _validator.ValidateServiceOffered(studioFacilityIds, requiredFacilityIds, isExcluded);
        if (!serviceResult.IsValid) return serviceResult;

        // Rule 5 — not during a closure
        IReadOnlyList<StudioClosure> closures = await _closureRepository.GetClosuresForStudioAsync(studioId);
        ValidationResult closureResult = _validator.ValidateNotDuringClosure(startDateTime, durationHours, closures);
        if (!closureResult.IsValid) return closureResult;

        // Rule 3 — buffer / overlap with existing bookings
        IReadOnlyList<Booking> existing = await _bookingRepository.GetConfirmedBookingsForStudioAsync(studioId);
        return _validator.ValidateNoOverlap(startDateTime, durationHours, existing);
    }

    // Validates, then persists the booking if every rule passes
    public async Task<(ValidationResult Validation, Booking? Booking)> CreateBookingAsync(
        int studioId, int userId, int serviceTypeId, DateTime startDateTime, int durationHours)
    {
        ValidationResult validation = await ValidateBookingAsync(studioId, serviceTypeId, startDateTime, durationHours);
        if (!validation.IsValid)
        {
            return (validation, null);
        }

        Booking booking = new()
        {
            StudioId = studioId,
            UserId = userId,
            ServiceTypeId = serviceTypeId,
            StartDateTime = startDateTime,
            DurationHours = durationHours,
            Status = BookingStatus.Confirmed
        };

        Booking saved = await _bookingRepository.AddAsync(booking);
        return (validation, saved);
    }

    public Task<IReadOnlyList<Booking>> GetBookingsForStudioAsync(int studioId) =>
        _bookingRepository.GetConfirmedBookingsForStudioAsync(studioId);
}
