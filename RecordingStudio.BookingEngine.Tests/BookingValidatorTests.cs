using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

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




        //[Fact]
        //public void ValidateStartTime_OnFullHour_ReturnSuccess()
        //{
        //    DateTime startTime = new(2026, 7    , 20,  14,  0,   0);
        //                           //year, month, day, hr, min, sec

        //    ValidationResult result = _validator.ValidateStartTime(startTime);

        //    Assert.True(result.IsValid);
        //}

        //[Fact]
        //public void ValidateStartTime_At15MinutesPast_ReturnsFail()
        //{
        //    DateTime startTime = new(2026, 7, 20, 14, 15, 0);
        //    ValidationResult result = _validator.ValidateStartTime(startTime);

        //    Assert.False(result.IsValid);

        //}


    }
}
