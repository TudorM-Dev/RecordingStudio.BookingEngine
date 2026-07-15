using RecordingStudio.BookingEngine.Core.Entities;
using RecordingStudio.BookingEngine.Core.Interfaces;

namespace RecordingStudio.BookingEngine.Core.Services;

public class StudioService
{
    private readonly IStudioRepository _studioRepository;
    private readonly IServiceTypeRepository _serviceTypeRepository;
    private readonly BookingValidator _validator;

    public StudioService(
        IStudioRepository studioRepository,
        IServiceTypeRepository serviceTypeRepository,
        BookingValidator validator)
    {
        _studioRepository = studioRepository;
        _serviceTypeRepository = serviceTypeRepository;
        _validator = validator;
    }

    public Task<IReadOnlyList<Studio>> GetStudiosAsync() => _studioRepository.GetAllAsync();

    // Rule 4 applied across all service types: which ones this studio can offer
    public async Task<IReadOnlyList<ServiceType>> GetOfferedServiceTypesAsync(int studioId)
    {
        IReadOnlyList<int> studioFacilityIds = await _studioRepository.GetFacilityIdsAsync(studioId);
        IReadOnlyList<int> excludedIds = await _studioRepository.GetExcludedServiceTypeIdsAsync(studioId);
        IReadOnlyList<ServiceType> allServices = await _serviceTypeRepository.GetAllAsync();

        List<ServiceType> offered = new();
        foreach (ServiceType service in allServices)
        {
            IReadOnlyList<int> required = await _serviceTypeRepository.GetRequiredFacilityIdsAsync(service.Id);
            bool isExcluded = excludedIds.Contains(service.Id);

            if (_validator.ValidateServiceOffered(studioFacilityIds, required, isExcluded).IsValid)
            {
                offered.Add(service);
            }
        }

        return offered;
    }
}
