using AutoMapper;

namespace StaffManagement.Designations;
public class DesignationService : BaseCrudService<
    Designation,
    DesignationDto,
    Guid,
    DesignationRequestDto,
    CreateDesignationDto,
    UpdateDesignationDto>, IDesignationService
{
    private readonly IDesignationRepository _designationRepository;

    public DesignationService(IMapper mapper, IDesignationRepository designationRepository) : base(mapper, designationRepository)
    {
        _designationRepository = designationRepository;
    }

}
