
using StaffManagement.Staffs;

namespace StaffManagement.MVC.Services;

public class StaffService : IStaffService
{
    private readonly IBaseService _baseService;

    public StaffService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto?> CreateAsync(CreateStaffDto input)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Data = input,
            Url = SD.StaffAPIBase + "/api/Staff"
        });
    }

    public async Task<ResponseDto?> DeleteAsync(Guid id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = SD.StaffAPIBase + "/api/Staff/" + id
        });
    }

    public async Task<ResponseDto?> GetAsync(Guid id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.StaffAPIBase + "/api/Staff/" + id
        });
    }

    public async Task<ResponseDto?> GetListAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = SD.StaffAPIBase + "/api/Staff"
        });
    }

    public async Task<ResponseDto?> UpdateAsync(UpdateStaffDto input)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.PUT,
            Data = input,
            Url = SD.StaffAPIBase + "/api/Staff/" + input.Id
        });
    }
}
