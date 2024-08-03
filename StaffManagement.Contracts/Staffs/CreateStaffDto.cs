using System.ComponentModel.DataAnnotations;

namespace StaffManagement.Staffs;

public record CreateStaffDto
{
    [Required]
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public int Number { get; set; }
    [Required]
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public List<Guid>? DesignationIds { get; set; }
}