namespace StaffManagement;
public record PagedResultDto<TEntity>
{
	public List<TEntity>? Items { get; set; }
    public int TotalCount { get; set; }
}
