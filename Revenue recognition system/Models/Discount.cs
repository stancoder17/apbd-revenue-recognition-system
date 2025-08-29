namespace Revenue_recognition_system.Models;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Percentage { get; set; }
    public DateTime ActiveFrom { get; set; }
    public DateTime ActiveTo { get; set; }
    public bool IsGlobal { get; set; }

    public virtual ICollection<ClientDiscount> ClientDiscounts { get; set; } = null!;
    public virtual ICollection<SoftwareVersionDiscount> SoftwareVersionDiscounts { get; set; } = null!;
}