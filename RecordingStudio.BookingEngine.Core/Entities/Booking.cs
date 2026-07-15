using RecordingStudio.BookingEngine.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;

        public DateTime StartDateTime { get; set; }
        public int DurationHours { get; set; }

        public BookingStatus Status { get; set; }
    }
}
