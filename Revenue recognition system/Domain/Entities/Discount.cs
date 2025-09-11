namespace Revenue_recognition_system.Domain.Entities;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Percentage { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime ActiveTo { get; set; }
    public bool IsGlobal { get; set; }

    public virtual ICollection<ClientDiscount> ClientDiscounts { get; set; } = new List<ClientDiscount>();

    public virtual ICollection<SoftwareVersionDiscount> SoftwareVersionDiscounts { get; set; } =
        new List<SoftwareVersionDiscount>();
}