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

    public async Task<Booking> AddAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return booking;
    }

    public async Task CancelAsync(IEnumerable<Booking> bookings)
    {
        foreach (Booking booking in bookings)
        {
            booking.Status = BookingStatus.Cancelled;
        }

        await _context.SaveChangesAsync();
    }
}
