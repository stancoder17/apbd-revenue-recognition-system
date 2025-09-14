namespace Revenue_recognition_system.Services.DTOs;

public class GetIndividualClientDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public GetAddressDto Address { get; set; } = null!;
}