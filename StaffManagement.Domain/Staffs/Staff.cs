namespace StaffManagement.Staffs;
public class Staff
{
    public Staff()
    {
    }
    public Guid Id { get; internal set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public int Number { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    public ICollection<StaffDesignation>? StaffDesignations { get; set; }

    /// <summary>
    /// In case we are using lazy loading
    /// </summary>
    //public virtual ICollection<StaffDesignation> StaffDesignations { get; set; }


    public void AddStaffDesignation(StaffDesignation staffDesignation)
    {
        StaffDesignations ??= [];
        StaffDesignations.Add(staffDesignation);
    }
}
