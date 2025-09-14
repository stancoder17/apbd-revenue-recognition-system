namespace Revenue_recognition_system.Services.DTOs;

public class GetAddressDto
{
    public int Id { get; set; }
    public string City { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Number { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}