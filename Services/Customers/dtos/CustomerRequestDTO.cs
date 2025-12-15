using System.ComponentModel.DataAnnotations;

namespace Blazor_FE.Services.Customers.dtos
{
    public class CustomerRequestDTO
    {
        [MinLength(3, ErrorMessage = "Tên khách hàng phải có ít nhất 3 ký tự.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được trống.")]
        [RegularExpression(@"^\d{9,11}$", ErrorMessage = "Số điện thoại phải bao gồm 9 đến 11 số.")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string? Email { get; set; }

        [MinLength(3, ErrorMessage = "Địa chỉ phải có ít nhất 3 ký tự.")]
        public string? Address { get; set; }
        
        public int AccountId { get; set; }
    }
}
