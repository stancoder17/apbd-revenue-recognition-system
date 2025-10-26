namespace Revenue_recognition_system.Services.DTOs;

public class AddContractDto
{
    public int ClientId { get; set; }
    public int SoftwareVersionId { get; set; }
    public DateTime EndDate { get; set; }
    public int AdditionalSupportYears { get; set; }
}