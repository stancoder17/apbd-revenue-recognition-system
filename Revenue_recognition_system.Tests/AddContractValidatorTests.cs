using FluentValidation.TestHelper;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Validators;

namespace Revenue_recognition_system.Tests;

public class AddContractValidatorTests
{
    private readonly AddContractValidator _validator = new();

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Should_Have_Error_When_ClientId_Not_Greater_Than_Zero(int clientId)
    {
        // Arrange
        var model = new AddContractDto
        {
            ClientId = clientId,
            SoftwareVersionId = 1,
            EndDate = DateTime.UtcNow.Date.AddDays(10),
            AdditionalSupportYears = 1
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ClientId);
    }
    
    [Fact]
    public void Should_Have_Error_When_ClientId_Not_Provided()
    {
        // Arrange
        var model = new AddContractDto
        {
            SoftwareVersionId = 1,
            EndDate = DateTime.UtcNow.Date.AddDays(10),
            AdditionalSupportYears = 1
        };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ClientId);
    }
    
    [Fact]
    public void Should_Have_Error_When_SoftwareVersionId_Not_Provided()
    {
        // Arrange
        var model = new AddContractDto
        {
            ClientId = 1,
            EndDate = DateTime.UtcNow.Date.AddDays(10),
            AdditionalSupportYears = 1
        };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SoftwareVersionId);
    }
    
    [Fact]
    public void Should_Have_Error_When_EndDate_Not_Provided()
    {
        // Arrange
        var model = new AddContractDto
        {
            ClientId = 1,
            SoftwareVersionId = 1,
            AdditionalSupportYears = 1
        };
        
        // Act
        var result = _validator.TestValidate(model);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.EndDate);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Should_Have_Error_When_SoftwareVersionId_Not_Greater_Than_Zero(int svId)
    {
        var model = new AddContractDto
        {
            ClientId = 1,
            SoftwareVersionId = svId,
            EndDate = DateTime.UtcNow.Date.AddDays(10),
            AdditionalSupportYears = 1
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.SoftwareVersionId);
    }

    [Theory]
    [InlineData(4)]
    [InlineData(-1)]
    public void Should_Have_Error_When_AdditionalSupportYears_Is_Not_0_1_2_Or_3(int years)
    {
        var model = new AddContractDto
        {
            ClientId = 1,
            SoftwareVersionId = 1,
            EndDate = DateTime.UtcNow.Date.AddDays(10),
            AdditionalSupportYears = years
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.AdditionalSupportYears);
    }

    [Fact]
    public void Should_Have_Error_When_EndDate_Is_Default()
    {
        var model = new AddContractDto
        {
            ClientId = 1,
            SoftwareVersionId = 1,
            EndDate = default, // NotEmpty should flag default(DateTime)
            AdditionalSupportYears = 1
        };

        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.EndDate);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void Should_Not_Have_Error_For_Valid_Input(int years)
    {
        var model = new AddContractDto
        {
            ClientId = 10,
            SoftwareVersionId = 20,
            EndDate = DateTime.UtcNow.Date.AddDays(10), // service checks 3..30 window, validator only checks non-empty
            AdditionalSupportYears = years
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
