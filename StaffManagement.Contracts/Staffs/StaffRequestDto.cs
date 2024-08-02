namespace StaffManagement.Staffs;

public record StaffRequestDto : PagedAndSortedRequestDto
{
    public string? Search { get; set; }

    //Other properties
}