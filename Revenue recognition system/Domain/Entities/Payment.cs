namespace Revenue_recognition_system.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public int StatusId { get; set; }
    public int PaymentMethodId { get; set; }
    public int PaymentTargetId { get; set; }

    public virtual Status Status { get; set; } = null!;
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    public virtual PaymentTarget PaymentTarget { get; set; } = null!;
}