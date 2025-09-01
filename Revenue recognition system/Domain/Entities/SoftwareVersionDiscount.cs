namespace Revenue_recognition_system.Models;

public class SoftwareVersionDiscount
{
    public int Id { get; set; }
    public int SoftwareVersionId { get; set; }
    public int DiscountId { get; set; }

    public virtual SoftwareVersion SoftwareVersion { get; set; } = null!;
    public virtual Discount Discount { get; set; } = null!;
}