using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class StudioClosure
    {
        public int Id { get; set; }
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string? Reason { get; set; } //Optional - ?

    }
}
