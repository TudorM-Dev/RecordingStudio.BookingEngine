namespace RecordingStudio.BookingEngine.Api.Contracts;

public class BookingRequest
{
    public int StudioId { get; set; }
    public int UserId { get; set; }
    public int ServiceTypeId { get; set; }
    public DateTime StartDateTime { get; set; }
    public int DurationHours { get; set; }
}
