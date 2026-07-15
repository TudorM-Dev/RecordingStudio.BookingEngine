using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class ServiceType
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<ServiceTypeRequiredFacility> ServiceTypeRequiredFacility { get; set; } = new List<ServiceTypeRequiredFacility>();
        public ICollection<StudioServiceExclusion> StudioServiceExclusions { get; set; } = new List<StudioServiceExclusion>();

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
