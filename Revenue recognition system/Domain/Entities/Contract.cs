namespace Revenue_recognition_system.Domain.Entities;

public class Contract
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareVersionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal FinalPrice { get; set; }
    public int? AdditionalSupportYears { get; set; }
    public DateTime? SignedAt { get; set; }

    public virtual Client Client { get; set; } = null!;
}