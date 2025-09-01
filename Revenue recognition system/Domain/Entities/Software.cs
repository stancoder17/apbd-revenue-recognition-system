namespace Revenue_recognition_system.Models;

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CategoryId { get; set; }
    public decimal LicensePrice { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<SoftwareVersion> Versions { get; set; } = new List<SoftwareVersion>();
}