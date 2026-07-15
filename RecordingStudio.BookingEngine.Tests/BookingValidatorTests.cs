using RecordingStudio.BookingEngine.Core.Common;
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
            // Act
            ValidationResult result = _validator.ValidateDuration(durationHours);

            // Assert
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
