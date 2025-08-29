namespace Revenue_recognition_system.Models;

public class PaymentTarget
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public int SupportExtensionId { get; set; }
    
    
    public virtual Contract Contract { get; set; } = null!;
    public virtual SupportExtension SupportExtension { get; set; } = null!;
    public virtual ICollection<Payment> Payments { get; set; } = null!;
}