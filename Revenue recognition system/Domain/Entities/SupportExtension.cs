namespace Revenue_recognition_system.Domain.Entities;

public class SupportExtension
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public int NumberOfYears { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
