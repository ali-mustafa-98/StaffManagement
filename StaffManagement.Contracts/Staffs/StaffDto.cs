namespace StaffManagement.Staffs;
public record StaffDto : BaseEntityDto<Guid>
{
	public required string FirstName { get; set; }
	public string? LastName { get; set; }
	public int Number { get; set; }
	public required string Email { get; set; }
	public string? PhoneNumber { get; set; }
}