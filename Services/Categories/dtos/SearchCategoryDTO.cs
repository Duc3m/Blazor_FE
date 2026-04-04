using Blazor_FE.Services.Base;

namespace Blazor_FE.Services.Categories.dtos;

public class SearchCategoryDTO : PageRequest
{
    public string? CategoryName { get; set; }
}