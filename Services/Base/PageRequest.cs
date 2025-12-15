namespace Blazor_FE.Services.Base;

public partial class PageRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}