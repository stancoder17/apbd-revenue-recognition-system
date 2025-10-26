namespace Revenue_recognition_system.Domain.Constants;

public static class ContractConstraints
{
    public const int AdditionalSupportYearsMinValue = 0;
    public const int AdditionalSupportYearsMaxValue = 3;
    public const decimal AdditionalSupportYearsPricePerYear = 1000m;
    public const decimal PricePerDayMultiplier = 0.01m;

    public const int DaysMinValue = 3;
    public const int DaysMaxValue = 30;
}