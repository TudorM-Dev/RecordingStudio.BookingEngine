using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Services;

namespace RecordingStudio.BookingEngine.Tests
{
    public class BookingValidatorTests
    {
        private readonly BookingValidator _validator = new();

        [Theory]
        [InlineData(14, 0, true)]
        [InlineData(14, 30, true)]
        [InlineData(14, 15, false)]
        [InlineData(14, 45, false)]
        [InlineData(14, 1, false)]
        public void ValidateStartTime_ChecksHalfHourBoundary(int hour, int minute, bool expectedValid)
        {
            DateTime startTime = new(2026, 7, 20, hour, minute, 0);

            ValidationResult result = _validator.ValidateStartTime(startTime);

            Assert.Equal(expectedValid, result.IsValid);
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(1, false)]
        [InlineData(0, false)]
        [InlineData(-1, false)]
        public void ValidateDuration_ChecksMinimumTwoHours(int durationHours, bool expectedValid)
        {
            ValidationResult result = _validator.ValidateDuration(durationHours);

            Assert.Equal(expectedValid, result.IsValid);
        }

        // Helper: build a booking with only the fields the overlap check cares about
        private static Booking BookingAt(DateTime start, int durationHours) =>
            new() { StartDateTime = start, DurationHours = durationHours };

        [Fact]
        public void ValidateNoOverlap_WithNoExistingBookings_ReturnsSuccess()
        {
            DateTime start = new(2026, 7, 20, 14, 0, 0);
            List<Booking> existing = new();

            ValidationResult result = _validator.ValidateNoOverlap(start, 2, existing);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(10, 0, true)]
        [InlineData(16, 30, true)]
        [InlineData(16, 0, false)]
        [InlineData(16, 15, false)]
        [InlineData(15, 0, false)]
        public void ValidateNoOverlap_ChecksBufferAgainstExistingBooking(int hour, int minute, bool expectedValid)
        {
            List<Booking> existing = new() { BookingAt(new DateTime(2026, 7, 20, 14, 0, 0), 2) };
            DateTime candidateStart = new(2026, 7, 20, hour, minute, 0);

            ValidationResult result = _validator.ValidateNoOverlap(candidateStart, 2, existing);

            Assert.Equal(expectedValid, result.IsValid);
        }

        // Rule 4: service offered from facilities + exclusion
        [Fact]
        public void ValidateServiceOffered_WhenStudioHasAllFacilitiesAndNotExcluded_ReturnsSuccess()
        {
            int[] studioFacilities = { 1, 2, 3 };
            int[] required = { 1, 2 };

            ValidationResult result = _validator.ValidateServiceOffered(studioFacilities, required, isServiceExcluded: false);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ValidateServiceOffered_WhenMissingARequiredFacility_ReturnsFailure()
        {
            int[] studioFacilities = { 1, 2, 3 };
            int[] required = { 1, 2, 4 };

            ValidationResult result = _validator.ValidateServiceOffered(studioFacilities, required, isServiceExcluded: false);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void ValidateServiceOffered_WhenExcluded_ReturnsFailure()
        {
            int[] studioFacilities = { 1, 2, 3 };
            int[] required = { 1, 2 };

            ValidationResult result = _validator.ValidateServiceOffered(studioFacilities, required, isServiceExcluded: true);

            Assert.False(result.IsValid);
        }

        // Rule 5: not during a closure
        private static StudioClosure ClosureFrom(DateTime start, DateTime end) =>
            new() { StartDateTime = start, EndDateTime = end };

        [Theory]
        // Closure runs 14:00–16:00
        [InlineData(10, true)]   // fully before -> valid
        [InlineData(16, true)]   // starts exactly when the closure ends -> valid
        [InlineData(14, false)]  // starts inside the closure -> invalid
        [InlineData(15, false)]  // overlaps the closure -> invalid
        public void ValidateNotDuringClosure_ChecksOverlap(int hour, bool expectedValid)
        {
            List<StudioClosure> closures = new()
            {
                ClosureFrom(new DateTime(2026, 7, 20, 14, 0, 0), new DateTime(2026, 7, 20, 16, 0, 0))
            };
            DateTime candidateStart = new(2026, 7, 20, hour, 0, 0);

            ValidationResult result = _validator.ValidateNotDuringClosure(candidateStart, 2, closures);

            Assert.Equal(expectedValid, result.IsValid);
        }
    }
}
