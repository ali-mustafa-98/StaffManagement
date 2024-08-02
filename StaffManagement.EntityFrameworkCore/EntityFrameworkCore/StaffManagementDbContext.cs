using Microsoft.EntityFrameworkCore;
using StaffManagement.Staffs;

namespace StaffManagement;
public class StaffManagementDbContext : DbContext
{
    public StaffManagementDbContext(DbContextOptions<StaffManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Staff> Staffs { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Staff>(x =>
		{
			x.HasKey("Id");
		});
	}
}