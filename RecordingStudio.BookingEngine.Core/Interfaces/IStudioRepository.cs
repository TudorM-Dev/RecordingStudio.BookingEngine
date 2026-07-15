using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Core.Interfaces
{
    public interface IStudioRepository
    {
        Task<IReadOnlyList<Studio>> GetAllAsync();

        Task<Studio?> GetByIdAsync(int studioId);

        // Ids of the facilities this studio has
        Task<IReadOnlyList<int>> GetFacilityIdsAsync(int studioId);

        // Ids of the service types this studio manually excludes
        Task<IReadOnlyList<int>> GetExcludedServiceTypeIdsAsync(int studioId);

        Task<bool> IsServiceExcludedAsync(int studioId, int serviceTypeId);
    }
}
