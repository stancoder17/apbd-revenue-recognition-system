namespace Revenue_recognition_system.Services.DTOs;

public class GetContractDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareVersionId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal FinalPrice { get; set; }
    public int? AdditionalSupportYears { get; set; }
    public DateTime? SignedAt { get; set; }
}
