namespace StaffManagement;

public record ApiResponse<TData>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; } = string.Empty;
    public TData? Data { get; set; }
}

public record ApiListResponse<TData>
{
    public int TotalCount { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; } = string.Empty;
    public IEnumerable<TData?>? Data { get; set; }
}
