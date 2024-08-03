namespace StaffManagement;

public class ResponseDto
{
    public int TotalCount { get; set; }
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; } = string.Empty;
    public object? Data { get; set; }
}