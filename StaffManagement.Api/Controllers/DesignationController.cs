using Microsoft.AspNetCore.Mvc;
using StaffManagement.Designations;

namespace StaffManagement.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DesignationController : ControllerBase
{
    private readonly IDesignationService _designationService;

    public DesignationController(IDesignationService designationService)
    {
        _designationService = designationService;
    }

    [HttpGet("/api/[controller]/{id}")]
    public async Task<ApiResponse<DesignationDto>> GetAsync(Guid id)
    {
        var designation = await _designationService.GetAsync(id);
        return new()
        {
            IsSuccess = true,
            StatusCode = StatusCodes.Status200OK,
            Data = designation
        };
    }

    [HttpDelete("/api/[controller]/{id}")]
    public async Task<ApiResponse<DesignationDto>> DeleteAsync(Guid id)
    {
        await _designationService.DeleteAsync(id);

        return new()
        {
            Data = null,
            IsSuccess = true,
            StatusCode = StatusCodes.Status204NoContent
        };
    }
    [HttpPost("/api/[controller]")]
    public async Task<ApiResponse<DesignationDto>> CreateAsync([FromBody] CreateDesignationDto input)
    {
        var designationDto = await _designationService.CreateAsync(input);
        return new()
        {
            Data = designationDto,
            StatusCode = StatusCodes.Status201Created,
            IsSuccess = true
        };
    }

    [HttpPut("/api/[controller]/{id}")]
    public async Task<ApiResponse<DesignationDto>> UpdateAsync(Guid id, [FromBody] UpdateDesignationDto input)
    {
        var designationDto = await _designationService.UpdateAsync(id, input);
        return new()
        {
            Data = designationDto,
            StatusCode = StatusCodes.Status201Created,
            IsSuccess = true
        };
    }
    [HttpGet("/api/[controller]")]
    public async Task<ApiListResponse<DesignationDto>> GetListAsync([FromQuery] DesignationRequestDto input)
    {
        var result = await _designationService.GetListAsync(input);

        return new()
        {
            Data = result?.Items ?? [],
            StatusCode = StatusCodes.Status200OK,
            TotalCount = result?.TotalCount ?? 0
        };
    }
}