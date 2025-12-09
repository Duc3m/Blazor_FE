namespace Blazor_FE.Services.Base;

public class APIResponse<T>
{
    public T? Content { get; set;}
    public Metadata Metadata { get; set; }
    public int StatusCode { get; set; }
    public String Message { get; set; }
}

public class Metadata
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalElements { get; set; }
}