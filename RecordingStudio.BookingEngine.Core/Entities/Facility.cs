using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class Facility
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<StudioFacility> StudioFacilities { get; set; } = new List<StudioFacility>();
        public ICollection<ServiceTypeRequiredFacility> ServiceTypeRequiredFacilities { get; set; } = new List<ServiceTypeRequiredFacility>();
    }
}
