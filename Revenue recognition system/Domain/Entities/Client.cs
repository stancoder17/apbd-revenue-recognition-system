namespace Revenue_recognition_system.Domain.Entities;

public abstract class Client
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int AddressId { get; set; }
    
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<ClientDiscount> ClientDiscounts { get; set; } = new List<ClientDiscount>();
    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}