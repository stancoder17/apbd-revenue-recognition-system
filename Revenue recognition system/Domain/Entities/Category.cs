namespace Revenue_recognition_system.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Software> Softwares { get; set; } = new List<Software>();
}