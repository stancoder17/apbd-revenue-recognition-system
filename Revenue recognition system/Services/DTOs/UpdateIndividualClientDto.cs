namespace Revenue_recognition_system.Services.DTOs;

public class UpdateIndividualClientDto
{
    // PESEL is immutable and intentionally omitted
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int AddressId { get; set; }
}
