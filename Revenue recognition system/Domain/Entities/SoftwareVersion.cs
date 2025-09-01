namespace Revenue_recognition_system.Models;

public class SoftwareVersion
{
    public int Id { get; set; }
    public int SoftwareId { get; set; }
    public string VersionNumber { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }

    public virtual Software Software { get; set; } = null!;
    public virtual ICollection<SoftwareVersionDiscount> SoftwareVersionDiscounts { get; set; } = new List<SoftwareVersionDiscount>();
}