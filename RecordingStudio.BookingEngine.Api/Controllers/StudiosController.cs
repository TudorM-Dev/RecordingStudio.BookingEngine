using Microsoft.AspNetCore.Mvc;
using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Services;

namespace RecordingStudio.BookingEngine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudiosController : ControllerBase
{
    private readonly StudioService _studioService;

    public StudiosController(StudioService studioService)
    {
        _studioService = studioService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IReadOnlyList<Studio> studios = await _studioService.GetStudiosAsync();
        return Ok(studios.Select(s => new { s.Id, s.Name, s.Sector }));
    }

    [HttpGet("{studioId:int}/services")]
    public async Task<IActionResult> GetOfferedServices(int studioId)
    {
        IReadOnlyList<ServiceType> services = await _studioService.GetOfferedServiceTypesAsync(studioId);
        return Ok(services.Select(s => new { s.Id, s.Name, s.Description }));
    }
}
