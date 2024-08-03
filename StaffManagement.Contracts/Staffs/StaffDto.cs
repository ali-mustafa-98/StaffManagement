using StaffManagement.Designations;

namespace StaffManagement.Staffs;
public record StaffDto : BaseEntityDto<Guid>
{
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public int Number { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
    public List<DesignationDto>? Designations { get; set; }
}