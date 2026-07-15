using Microsoft.EntityFrameworkCore;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;
using RecordingStudio.BookingEngine.Infrastructure.Data;

namespace RecordingStudio.BookingEngine.Infrastructure.Repositories;

public class ServiceTypeRepository : IServiceTypeRepository
{
    private readonly BookingEngineDbContext _context;

    public ServiceTypeRepository(BookingEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<ServiceType>> GetAllAsync()
    {
        return await _context.ServiceTypes.ToListAsync();
    }

    public async Task<ServiceType?> GetByIdAsync(int serviceTypeId)
    {
        return await _context.ServiceTypes.FindAsync(serviceTypeId);
    }

    public async Task<IReadOnlyList<int>> GetRequiredFacilityIdsAsync(int serviceTypeId)
    {
        return await _context.ServiceTypeRequiredFacilities
            .Where(srf => srf.ServiceTypeId == serviceTypeId)
            .Select(srf => srf.FacilityId)
            .ToListAsync();
    }
}
