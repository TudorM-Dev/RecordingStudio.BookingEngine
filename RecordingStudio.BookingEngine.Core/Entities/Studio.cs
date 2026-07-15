using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Entities
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;

        public ICollection<StudioFacility> StudioFacilities { get; set; } = new List<StudioFacility>();
        public ICollection<StudioServiceExclusion> StudioServiceExclusions { get; set; } = new List<StudioServiceExclusion>();
        public ICollection<StudioClosure> StudioClosures { get; set; } = new List<StudioClosure>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
