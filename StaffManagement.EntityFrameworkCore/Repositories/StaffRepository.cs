
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
		//Load the nav properties (in case we need them)
		//Here we are using eager loading (I usually use lazy loading but I thought this may be more simple for the small demo)
		query = query.Include(x => x.StaffDesignations)
						.ThenInclude(x => x.Designation);
		return query.AsNoTracking();
	}
}
