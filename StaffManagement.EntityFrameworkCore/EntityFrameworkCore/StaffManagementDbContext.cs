using Microsoft.EntityFrameworkCore;
using StaffManagement.Designations;
using StaffManagement.Staffs;

namespace StaffManagement;
public class StaffManagementDbContext : DbContext
{
    public StaffManagementDbContext(DbContextOptions<StaffManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Staff> Staffs { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<StaffDesignation> StaffDesignations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Configure tables
    }
}
