namespace StaffManagement.Staffs;
public class Staff
{
    public Staff()
    {
    }
    public Guid Id { get; set; }
	public required string FirstName { get; set; }
	public string? LastName { get; set; }
	public int Number { get; set; }
	public required string Email { get; set; }
	public string? PhoneNumber { get; set; }


}
