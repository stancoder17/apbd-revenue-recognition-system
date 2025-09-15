using FluentValidation.TestHelper;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Validators;

namespace Revenue_recognition_system.Tests;

public class CompanyClientValidatorTests
{
    private readonly CompanyClientValidator _validator = new();

    // Each InlineData row = one invalid case for a specific property
    [Theory]
    [InlineData("", "1234567890", "test@test.com", "123456789", 1, "Name")]                 // empty name
    [InlineData("NameIsWayTooLong____________________________________________________________________________________________________",
        "1234567890", "test@test.com", "123456789", 1, "Name")]                 // too long name
    [InlineData("ValidName", "", "test@test.com", "123456789", 1, "Krs")]                   // empty KRS
    [InlineData("ValidName", "123", "test@test.com", "123456789", 1, "Krs")]                // short KRS
    [InlineData("ValidName", "413278843126", "test@test.com", "123456789", 1, "Krs")]                // too long KRS
    [InlineData("ValidName", "41327jf431", "test@test.com", "123456789", 1, "Krs")]                // KRS contains non-digit character(s)
    [InlineData("ValidName", "1234567890", "", "123456789", 1, "Email")]                    // empty email
    [InlineData("ValidName", "1234567890", "EmailIsWayTooLong_____________________________________________________________________________________________________________________________________________________________________________________@.com",
        "123456789", 1, "Email")] // too long email
    [InlineData("ValidName", "1234567890", "bad-email", "123456789", 1, "Email")]           // invalid email format
    [InlineData("ValidName", "1234567890", "test@test.com", "", 1, "PhoneNumber")]          // empty phone
    [InlineData("ValidName", "1234567890", "test@test.com", "+48 912 491 412 943", 1, "PhoneNumber")]          // too long phone
    [InlineData("ValidName", "1234567890", "test@test.com", "123456789", 0, "AddressId")]   // invalid AddressId
    public void Should_Have_Error_For_Invalid_Input(
        string name, string krs, string email, string phoneNumber, int addressId, string expectedErrorProperty)
    {
        // Arrange
        var model = new AddCompanyClientDto
        {
            Name = name,
            Krs = krs,
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
        var model = new AddCompanyClientDto
        {
            Name = "Valid Company Name",
            Krs = new string('1', ClientConstraints.KrsLength),
            Email = "valid@email.com",
            PhoneNumber = "123456789",
            AddressId = 1
        };

        var result = _validator.TestValidate(model);

        result.ShouldNotHaveAnyValidationErrors();
    }

}

