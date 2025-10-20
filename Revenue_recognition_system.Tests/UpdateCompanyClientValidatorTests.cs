using FluentValidation.TestHelper;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Validators;
using Xunit;

namespace Revenue_recognition_system.Tests;

public class UpdateCompanyClientValidatorTests
{
    private readonly UpdateCompanyClientValidator _validator = new();

    // Each InlineData row = one invalid case for a specific property
    [Theory]
    [InlineData("", "test@test.com", "123456789", 1, "Name")]                 // name empty
    [InlineData("NameIsWayTooLong____________________________________________________________________________________________________",
        "test@test.com", "123456789", 1, "Name")]                 // name too long
    [InlineData("ValidName", "", "123456789", 1, "Email")]                    // email empty
    [InlineData("ValidName", "bad-email", "123456789", 1, "Email")]           // email invalid
    [InlineData("ValidName", "EmailIsWayTooLong_____________________________________________________________________________________________________________________________________________________________________________________@.com",
        "123456789", 1, "Email")] // email too long
    [InlineData("ValidName", "test@test.com", "", 1, "PhoneNumber")]          // phone empty
    [InlineData("ValidName", "test@test.com", "+48 912 491 412 943 123", 1, "PhoneNumber")]          // phone too long
    [InlineData("ValidName", "test@test.com", "123456789", 0, "AddressId")]   // AddressId invalid
    public void Should_Have_Error_For_Invalid_Input(
        string name, string email, string phoneNumber, int addressId, string expectedErrorProperty)
    {
        // Arrange
        var model = new UpdateCompanyClientDto
        {
            Name = name,
            Email = email,
            PhoneNumber = phoneNumber,
            AddressId = addressId
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldHaveValidationErrorFor(expectedErrorProperty);
    }
    
    [Fact]
    public void Should_Not_Have_Error_For_Valid_Input()
    {
        var model = new UpdateCompanyClientDto
        {
            Name = "Valid Company Name",
            Email = "valid@email.com",
            PhoneNumber = "123456789",
            AddressId = 1
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }
}