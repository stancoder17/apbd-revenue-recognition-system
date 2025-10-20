namespace Revenue_recognition_system.Services.DTOs;

public class UpdateCompanyClientDto
{
    // KRS is immutable and intentionally omitted
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int AddressId { get; set; }
}
