using RecordingStudio.BookingEngine.Core.Common;
using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Core.Services
{
    public class BookingValidator
    {
        // Rule 1: start time must fall on a 30-minute boundary (14:00, 14:30, ...)
        public ValidationResult ValidateStartTime(DateTime startDateTime)
        {
            bool isOnHalfHour = startDateTime.Minute is 0 or 30;
            bool hasNoSmallerParts = startDateTime.Second == 0 && startDateTime.Millisecond == 0;

            if (isOnHalfHour && hasNoSmallerParts) return ValidationResult.Success();
            return ValidationResult.Failure("Start time must be on a 30-minute boundary (e.g. 14:00 or 14:30).");
        }

        // Rule 2: minimum duration is 2 hours, in whole-hour steps (2h, 3h, 4h...)
        public ValidationResult ValidateDuration(int durationHours)
        {
            if (durationHours < 2)
            {
                return ValidationResult.Failure("Duration must be at least 2 hours.");
            }

            return ValidationResult.Success();
        }


        // Validates rules that depend only on the booking's own data (no db access)
        // Rule 1 && Rule 2
        public ValidationResult ValidateScheduleResult(DateTime startDateTime, int durationHours)
        {
            ValidationResult startTimeResult = ValidateStartTime(startDateTime);
            if (!startTimeResult.IsValid) return startTimeResult;

            ValidationResult durationResult = ValidateDuration(durationHours);
            if (!durationResult.IsValid) return durationResult;

            return ValidationResult.Success();
        }

        private static readonly TimeSpan Buffer = TimeSpan.FromMinutes(30);

        // Rule 3: at least a 30-minute gap between bookings at the same studio
        public ValidationResult ValidateNoOverlap(
            DateTime startDateTime,
            int durationHours,
            IEnumerable<Booking> existingBookings)
        {
            DateTime candidateStart = startDateTime;
            DateTime candidateEnd = startDateTime.AddHours(durationHours);

            foreach (Booking existing in existingBookings)
            {
                // Inflate each existing booking by the buffer on both sides
                DateTime blockedStart = existing.StartDateTime.Subtract(Buffer);
                DateTime blockedEnd = existing.StartDateTime.AddHours(existing.DurationHours).Add(Buffer);

                bool overlaps = candidateStart < blockedEnd && blockedStart < candidateEnd;
                if (overlaps)
                {
                    return ValidationResult.Failure("This time slot conflicts with an existing booking (including the required 30-minute buffer).");
                }
            }

            return ValidationResult.Success();
        }


    }
}