using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class ServiceTypeRequiredFacility
    {
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; } = null!;
        public int FacilityId { get; set; }
        public Facility Facility { get; set; } = null!;
    }
}
