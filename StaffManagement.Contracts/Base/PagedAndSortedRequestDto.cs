namespace StaffManagement;
/// <summary>
/// The base class which all request dtos will inherit from 
/// that is needed when we want to get list of entities
/// </summary>
public record PagedAndSortedRequestDto
{
	public int? SkipCount { get; set; }
    public int? MaxResultResult { get; set; }
    public string? Sorting { get; set; }
}
