using StaffManagement.Designations;

namespace StaffManagement.Staffs;
public class StaffDesignation
{
    /// <summary>
    /// For mapping purposes
    /// </summary>
    public StaffDesignation()
    {
    }

    public StaffDesignation(Guid id, Guid staffId, Guid designationId)
    {
        Id = id;
        StaffId = staffId;
        DesignationId = designationId;
    }

    public Guid Id { get; internal set; }
    public Guid StaffId { get; set; }
    public Staff? Staff { get; set; }
    public Guid DesignationId { get; set; }
    public Designation? Designation { get; set; }
}
