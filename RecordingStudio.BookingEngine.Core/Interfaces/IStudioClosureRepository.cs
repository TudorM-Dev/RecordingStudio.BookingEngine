using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Core.Interfaces
{
    public interface IStudioClosureRepository
    {
        Task<IReadOnlyList<StudioClosure>> GetClosuresForStudioAsync(int studioId);

        Task<StudioClosure> AddAsync(StudioClosure closure);
    }
}
