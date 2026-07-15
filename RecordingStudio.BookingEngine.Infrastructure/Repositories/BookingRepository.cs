using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Enums;
using RecordingStudio.BookingEngine.Core.Interfaces;
using RecordingStudio.BookingEngine.Infrastructure.Data;

namespace RecordingStudio.BookingEngine.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly BookingEngineDbContext _context;

    public BookingRepository(BookingEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Booking>> GetConfirmedBookingsForStudioAsync(int studioId)
    {
        return await _context.Bookings
            .Where(b => b.StudioId == studioId && b.Status == BookingStatus.Confirmed)
            .ToListAsync();
    }
}