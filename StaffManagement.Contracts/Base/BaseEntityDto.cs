namespace StaffManagement;
/// <summary>
/// This will be the base dto, all other dtos (I mean the output dtos) will inherit from it
/// right now we only have the "Id" property
/// later we can add other properties as needed like :
/// "CreationTime", "CreatorId", "LastModificationTime", "IsDeleted" (In case we are using soft delete) and so on ...
/// </summary>
/// <typeparam name="TKey"></typeparam>
public record BaseEntityDto<TKey>
{
    public TKey Id { get; set; }
}
