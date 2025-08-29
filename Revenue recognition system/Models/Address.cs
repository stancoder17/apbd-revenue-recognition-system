namespace Revenue_recognition_system.Models;

public class Address
{
    public int Id { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}