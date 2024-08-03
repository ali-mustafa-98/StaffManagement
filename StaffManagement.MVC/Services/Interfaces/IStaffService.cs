using StaffManagement.Staffs;

namespace StaffManagement;

public interface IStaffService
{
    Task<ResponseDto?> GetAsync(Guid id);
    Task<ResponseDto?> GetListAsync();
    Task<ResponseDto?> CreateAsync(CreateStaffDto input);
    Task<ResponseDto?> UpdateAsync(UpdateStaffDto input);
    Task<ResponseDto?> DeleteAsync(Guid id);
}
