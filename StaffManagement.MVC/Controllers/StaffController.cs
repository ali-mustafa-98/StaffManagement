using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StaffManagement.Staffs;

namespace StaffManagement.MVC.Controllers;
public class StaffController : Controller
{
    private readonly IStaffService _staffService;

    public StaffController(IStaffService staffService)
    {
        _staffService = staffService;
    }


    public async Task<IActionResult> Index()
    {
        List<StaffDto> list = [];

        ResponseDto? response = await _staffService.GetListAsync();

        if (response?.Data != null)
        {
            list = JsonConvert.DeserializeObject<List<StaffDto>>(Convert.ToString(response.Data));

        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(list);
    }


    public async Task<IActionResult> CreateAsync()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateStaffDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _staffService.CreateAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "staff created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }
        return View(model);
    }

    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        ResponseDto? response = await _staffService.GetAsync(id);

        if (response != null && response.IsSuccess)
        {
            StaffDto? model = JsonConvert.DeserializeObject<StaffDto>(Convert.ToString(response.Data));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(StaffDto model)
    {
        ResponseDto? response = await _staffService.DeleteAsync(model.Id);

        if (response != null && response.IsSuccess)
        {
            TempData["success"] = "staff deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return View(model);
    }

    public async Task<IActionResult> EditAsync(Guid id)
    {
        ResponseDto? response = await _staffService.GetAsync(id);

        if (response != null && response.IsSuccess)
        {
            UpdateStaffDto? model = JsonConvert.DeserializeObject<UpdateStaffDto>(Convert.ToString(response.Data));
            return View(model);
        }
        else
        {
            TempData["error"] = response?.Message;
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(UpdateStaffDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _staffService.UpdateAsync(model);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Staff updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
        }
        return View(model);
    }

}
