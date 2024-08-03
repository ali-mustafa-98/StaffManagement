namespace StaffManagement.Designations;
public interface IDesignationService
{
    Task<DesignationDto> GetAsync(Guid id);

    Task<DesignationDto> CreateAsync(CreateDesignationDto input);
    Task<DesignationDto> UpdateAsync(Guid id, UpdateDesignationDto input);
    Task DeleteAsync(Guid id);

    Task<PagedResultDto<DesignationDto>> GetListAsync(DesignationRequestDto input);

}
