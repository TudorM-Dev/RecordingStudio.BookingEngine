using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;
using RecordingStudio.BookingEngine.Infrastructure.Data;

namespace RecordingStudio.BookingEngine.Infrastructure.Repositories;

public class StudioRepository : IStudioRepository
{
    private readonly BookingEngineDbContext _context;

    public StudioRepository(BookingEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Studio>> GetAllAsync()
    {
        return await _context.Studios.ToListAsync();
    }

    public async Task<Studio?> GetByIdAsync(int studioId)
    {
        return await _context.Studios.FindAsync(studioId);
    }

    public async Task<IReadOnlyList<int>> GetFacilityIdsAsync(int studioId)
    {
        return await _context.StudioFacilities
            .Where(sf => sf.StudioId == studioId)
            .Select(sf => sf.FacilityId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<int>> GetExcludedServiceTypeIdsAsync(int studioId)
    {
        return await _context.StudioServiceExclusions
            .Where(sse => sse.StudioId == studioId)
            .Select(sse => sse.ServiceTypeId)
            .ToListAsync();
    }

    public async Task<bool> IsServiceExcludedAsync(int studioId, int serviceTypeId)
    {
        return await _context.StudioServiceExclusions
            .AnyAsync(sse => sse.StudioId == studioId && sse.ServiceTypeId == serviceTypeId);
    }
}
