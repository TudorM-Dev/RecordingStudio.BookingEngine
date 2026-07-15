using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Common
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string? Error { get; }

        private ValidationResult(bool isValid, string? error)
        {
            IsValid = isValid;
            Error = error;
        }

        public static ValidationResult Success() => new(true, null);
        public static ValidationResult Failure(string error) => new(false, error);
    }
}
