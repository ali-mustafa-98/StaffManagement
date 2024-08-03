namespace StaffManagement.Staffs;
public interface IStaffRepository : IRepository<Staff, Guid>
{
    IQueryable<Staff> WithNoTracking();
}
