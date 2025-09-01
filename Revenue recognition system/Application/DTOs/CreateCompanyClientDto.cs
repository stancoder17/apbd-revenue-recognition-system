namespace Revenue_recognition_system.Application.DTOs;

public class CreateCompanyClientDto
{
    public string Name { get; set; } = null!;
    public string Krs { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int AddressId { get; set; }
}