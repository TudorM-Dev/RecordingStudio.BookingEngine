using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;
using RecordingStudio.BookingEngine.Infrastructure.Data;

namespace RecordingStudio.BookingEngine.Infrastructure.Repositories;

public class StudioClosureRepository : IStudioClosureRepository
{
    private readonly BookingEngineDbContext _context;

    public StudioClosureRepository(BookingEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<StudioClosure>> GetClosuresForStudioAsync(int studioId)
    {
        return await _context.StudioClosures
            .Where(c => c.StudioId == studioId)
            .ToListAsync();
    }

    public async Task<StudioClosure> AddAsync(StudioClosure closure)
    {
        _context.StudioClosures.Add(closure);
        await _context.SaveChangesAsync();
        return closure;
    }
}
