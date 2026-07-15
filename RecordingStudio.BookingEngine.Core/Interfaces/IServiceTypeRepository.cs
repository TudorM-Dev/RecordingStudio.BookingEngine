using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Core.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<IReadOnlyList<ServiceType>> GetAllAsync();

        Task<ServiceType?> GetByIdAsync(int serviceTypeId);

        // Ids of the facilities this service type requires
        Task<IReadOnlyList<int>> GetRequiredFacilityIdsAsync(int serviceTypeId);
    }
}
