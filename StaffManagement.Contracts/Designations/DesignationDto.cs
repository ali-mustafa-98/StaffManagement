namespace StaffManagement.Designations;
public record DesignationDto : BaseEntityDto<Guid>
{
    public required string Name { get; set; }
}