using Microsoft.AspNetCore.SignalR;

namespace RecordingStudio.BookingEngine.Api.Hubs;

// Broadcasts booking changes so connected clients can refresh live.
// Clients listen for the "BookingsChanged" message carrying the affected studio id.
public class BookingHub : Hub
{
    public const string BookingsChanged = "BookingsChanged";
}
