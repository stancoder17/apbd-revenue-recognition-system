namespace Revenue_recognition_system.Services.DTOs;

public class GetCompanyClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Krs { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public GetAddressDto Address { get; set; } = null!;
}