namespace Revenue_recognition_system.Application.Validators;

public class ValidationResult
{
    public List<string> Errors { get; } = [];
    public bool IsValid => Errors.Count == 0;
}