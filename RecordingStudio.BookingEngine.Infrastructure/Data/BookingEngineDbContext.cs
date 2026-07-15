using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Infrastructure.Data
{
    public class BookingEngineDbContext : DbContext
    {

        public BookingEngineDbContext(DbContextOptions<BookingEngineDbContext> options) :base(options) { }

        public DbSet<Studio> Studios { get; set; } = null!;
        public DbSet<Facility> Facilities { get; set; } = null!;
        public DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        public DbSet<StudioFacility> StudioFacilities { get; set; } = null!;
        public DbSet<ServiceTypeRequiredFacility> ServiceTypeRequiredFacilities { get; set; } = null!;
        public DbSet<StudioServiceExclusion> StudioServiceExclusions { get; set; } = null!;
        public DbSet<StudioClosure> StudioClosures { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudioFacility>()
                .HasKey(sf => new {sf.StudioId, sf.FacilityId});

            modelBuilder.Entity<ServiceTypeRequiredFacility>()
                .HasKey(srf => new { srf.ServiceTypeId, srf.FacilityId });

            modelBuilder.Entity<StudioServiceExclusion>()
                .HasKey(sse => new { sse.StudioId, sse.ServiceTypeId });

            SeedData(modelBuilder);
        }

        // Reference data so the app is usable out of the box (versioned in a migration)
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facility>().HasData(
                new Facility { Id = 1, Name = "Microphone" },
                new Facility { Id = 2, Name = "Mixing Console" },
                new Facility { Id = 3, Name = "Vocal Booth" },
                new Facility { Id = 4, Name = "Grand Piano" });

            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType { Id = 1, Name = "Recording Session", Description = "Standard recording session" },
                new ServiceType { Id = 2, Name = "Mixing", Description = "Mixing an existing recording" },
                new ServiceType { Id = 3, Name = "Piano Recording", Description = "Recording session with grand piano" });

            modelBuilder.Entity<Studio>().HasData(
                new Studio { Id = 1, Name = "Studio A", Sector = "Downtown" },
                new Studio { Id = 2, Name = "Studio B", Sector = "Uptown" });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "Test Client", Email = "client@example.com" });

            // Studio A: Microphone, Mixing Console, Vocal Booth
            // Studio B: Microphone, Mixing Console, Grand Piano
            modelBuilder.Entity<StudioFacility>().HasData(
                new StudioFacility { StudioId = 1, FacilityId = 1 },
                new StudioFacility { StudioId = 1, FacilityId = 2 },
                new StudioFacility { StudioId = 1, FacilityId = 3 },
                new StudioFacility { StudioId = 2, FacilityId = 1 },
                new StudioFacility { StudioId = 2, FacilityId = 2 },
                new StudioFacility { StudioId = 2, FacilityId = 4 });

            // Recording -> Mic + Console; Mixing -> Console; Piano Recording -> Mic + Console + Piano
            modelBuilder.Entity<ServiceTypeRequiredFacility>().HasData(
                new ServiceTypeRequiredFacility { ServiceTypeId = 1, FacilityId = 1 },
                new ServiceTypeRequiredFacility { ServiceTypeId = 1, FacilityId = 2 },
                new ServiceTypeRequiredFacility { ServiceTypeId = 2, FacilityId = 2 },
                new ServiceTypeRequiredFacility { ServiceTypeId = 3, FacilityId = 1 },
                new ServiceTypeRequiredFacility { ServiceTypeId = 3, FacilityId = 2 },
                new ServiceTypeRequiredFacility { ServiceTypeId = 3, FacilityId = 4 });

            // Studio A manually excludes Mixing even though it has the console
            modelBuilder.Entity<StudioServiceExclusion>().HasData(
                new StudioServiceExclusion { StudioId = 1, ServiceTypeId = 2 });
        }
    }
}
