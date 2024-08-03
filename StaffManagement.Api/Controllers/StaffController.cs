using Microsoft.AspNetCore.Mvc;
using StaffManagement.Staffs;

namespace StaffManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class StaffController : ControllerBase
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }

    [HttpGet("/api/[controller]/{id}")]
    public async Task<ApiResponse<StaffDto>> GetAsync(Guid id)
    {
        var staff = await _staffService.GetAsync(id);
        return new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Data = staff
        };
    }

    [HttpDelete("/api/[controller]/{id}")]
    public async Task<ApiResponse<StaffDto>> DeleteAsync(Guid id)
    {
        await _staffService.DeleteAsync(id);

        return new()
        {
            Data = null,
            IsSuccess = true,
            StatusCode = StatusCodes.Status204NoContent
        };
    }
    [HttpPost("/api/[controller]")]
    public async Task<ApiResponse<StaffDto>> CreateAsync([FromBody] CreateStaffDto input)
    {
        var staffDto = await _staffService.CreateAsync(input);
        return new()
        {
            Data = staffDto,
            StatusCode = StatusCodes.Status201Created,
            IsSuccess = true
        };
    }

    [HttpPut("/api/[controller]/{id}")]
    public async Task<ApiResponse<StaffDto>> UpdateAsync(Guid id, [FromBody] UpdateStaffDto input)
    {
        var staffDto = await _staffService.UpdateAsync(id, input);
        return new()
        {
            Data = staffDto,
            StatusCode = StatusCodes.Status201Created,
            IsSuccess = true
        };
    }
    [HttpGet("/api/[controller]")]
    public async Task<ApiListResponse<StaffDto>> GetListAsync([FromQuery] StaffRequestDto input)
    {
        var result = await _staffService.GetListAsync(input);

        return new()
        {
            Data = result?.Items ?? [],
            StatusCode = StatusCodes.Status200OK,
            TotalCount = result?.TotalCount ?? 0
        };
    }
}
