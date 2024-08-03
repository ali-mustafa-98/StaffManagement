namespace StaffManagement;
/// <summary>
/// The result of get list for any api (In most cases we need to do paging)
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public record PagedResultDto<TEntity>
{
    public List<TEntity>? Items { get; set; }
    public int TotalCount { get; set; }
}
