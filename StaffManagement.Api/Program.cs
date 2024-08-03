using Microsoft.EntityFrameworkCore;
using StaffManagement;
using StaffManagement.Designations;
using StaffManagement.Repositories;
using StaffManagement.Staffs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("MyConnection");
if (connectionString is not null)
{
    builder.Services.AddDbContext<StaffManagementDbContext>(options =>
    {
        //options.UseLazyLoadingProxies();
        options.UseSqlServer(connectionString);
        options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Information)));
    });
}
builder.Services.AddTransient<IStaffRepository, StaffRepository>();

builder.Services.AddTransient<IStaffService, StaffService>();
builder.Services.AddTransient<IDesignationRepository, DesignationRepository>();
builder.Services.AddTransient<IDesignationService, DesignationService>();

//builder.Services.AddTransient<IStaffService, StaffService>();

builder.Services.AddAutoMapper(typeof(StaffManagementAutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
