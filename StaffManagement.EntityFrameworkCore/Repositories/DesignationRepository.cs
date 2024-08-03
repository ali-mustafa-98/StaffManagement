using StaffManagement.Designations;

namespace StaffManagement.Repositories;

public class DesignationRepository : EfCoreRepository<Designation, StaffManagementDbContext, Guid>, IDesignationRepository
{
	public DesignationRepository(StaffManagementDbContext context) : base(context)
	{
	}
}
