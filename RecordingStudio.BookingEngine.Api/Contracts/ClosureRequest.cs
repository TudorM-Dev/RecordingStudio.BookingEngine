namespace RecordingStudio.BookingEngine.Api.Contracts;

public class ClosureRequest
{
    public int StudioId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string? Reason { get; set; }
}
