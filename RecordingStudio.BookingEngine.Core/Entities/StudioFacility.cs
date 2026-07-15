using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class StudioFacility
    {
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;

        public int FacilityId { get; set; }
        public Facility Facility { get; set; } = null!;
    }
}
