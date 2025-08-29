namespace Revenue_recognition_system.Models;

public class ClientDiscount
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int DiscountId { get; set; }

    public virtual Client Client { get; set; } = null!;
    public virtual Discount Discount { get; set; } = null!;
}