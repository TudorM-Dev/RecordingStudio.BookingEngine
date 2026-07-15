using RecordingStudio.BookingEngine.Core.Entities;

namespace RecordingStudio.BookingEngine.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<IReadOnlyList<Booking>> GetConfirmedBookingsForStudioAsync(int studioId);

        // Persists a new booking and returns it with its generated Id
        Task<Booking> AddAsync(Booking booking);

        // Marks the given bookings as Cancelled and saves
        Task CancelAsync(IEnumerable<Booking> bookings);
    }
}
