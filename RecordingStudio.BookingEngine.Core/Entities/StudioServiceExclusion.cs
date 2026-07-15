using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class StudioServiceExclusion
    {
        public int StudioId { get; set; }
        public Studio Studio { get; set; } = null!;

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;



    }
}
