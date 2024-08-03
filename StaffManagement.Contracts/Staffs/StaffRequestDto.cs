namespace StaffManagement.Staffs;

public record StaffRequestDto : PagedAndSortedRequestDto
{
    public string? Search { get; set; }

    public List<Guid>? DesignationIds { get; set; }
    //Other filers will be here
}