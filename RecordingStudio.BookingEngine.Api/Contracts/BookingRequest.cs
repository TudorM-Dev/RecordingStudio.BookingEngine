namespace RecordingStudio.BookingEngine.Api.Contracts;

public class BookingRequest
{
    public int StudioId { get; set; }
    public DateTime StartDateTime { get; set; }
    public int DurationHours { get; set; }
}