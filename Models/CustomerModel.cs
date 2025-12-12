namespace Blazor_FE.Models;

public class CustomerModel
{
    public int CustomerId { get; set; }
    public int? AccountId { get; set; }
    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }
}
