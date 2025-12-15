using System.ComponentModel.DataAnnotations;

namespace Blazor_FE.Services.Categories.dtos;

public class CategoryDTO
{
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [MinLength(3, ErrorMessage = "Tên loại phải từ 3 ký tự trở lên")]
    public string CategoryName { get; set; } = null!;
}