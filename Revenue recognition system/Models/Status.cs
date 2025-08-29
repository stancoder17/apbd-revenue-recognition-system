namespace Revenue_recognition_system.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = null!;
}