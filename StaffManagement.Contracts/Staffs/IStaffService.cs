namespace StaffManagement.Staffs;
public interface IStaffService
{
	Task<StaffDto> GetAsync(Guid id);

	Task<StaffDto> CreateAsync(CreateStaffDto input);
	Task<StaffDto> UpdateAsync(Guid id, UpdateStaffDto input);
	Task DeleteAsync(Guid id);

	Task<PagedResultDto<StaffDto>> GetListAsync(StaffRequestDto input);

}