
using Microsoft.EntityFrameworkCore;
using StaffManagement.Staffs;

namespace StaffManagement.Repositories;
public class StaffRepository : EfCoreRepository<Staff, StaffManagementDbContext, Guid>, IStaffRepository
{
	private readonly StaffManagementDbContext _context;

	public StaffRepository(StaffManagementDbContext context) : base(context)
	{
		_context = context;
	}

	public IQueryable<Staff> WithNoTracking()
	{
		var query = GetQueryable();
		//query =  query.Include(x => x.SomeNavproperty);
		return query.AsNoTracking();
	}
}
