﻿namespace StaffManagement;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string? Url { get; set; }
    public object? Data { get; set; }
    public ContentType ContentType { get; set; } = ContentType.Json;
}