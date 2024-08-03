namespace StaffManagement.Designations;

public record DesignationRequestDto : PagedAndSortedRequestDto
{
    public string? Name { get; set; }
}
