using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

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
        }
    }
}
