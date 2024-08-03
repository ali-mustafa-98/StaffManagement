using StaffManagement.Staffs;

namespace StaffManagement.Designations;
public class Designation
{
    public Guid Id { get; internal set; }

    public required string Name { get; set; }

    public ICollection<StaffDesignation>? StaffDesignations { get; set; }

}
