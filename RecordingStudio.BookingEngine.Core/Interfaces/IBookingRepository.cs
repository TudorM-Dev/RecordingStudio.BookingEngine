using RecordingStudio.BookingEngine.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecordingStudio.BookingEngine.Core.Interfaces
{
    public interface IBookingRepository
    {
        Task<IReadOnlyList<Booking>> GetConfirmedBookingsForStudioAsync(int studioId);
    }
}
