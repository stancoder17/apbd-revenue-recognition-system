using FluentValidation.TestHelper;
using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Validators;
using Xunit;

namespace Revenue_recognition_system.Tests;

public class UpdateIndividualClientValidatorTests
{
    private readonly UpdateIndividualClientValidator _validator = new();

    // Each InlineData row = one invalid case for a specific property
    [Theory]
    [InlineData("", "Kowalski", "test@test.com", "123456789", 1, "FirstName")]  // first name empty
    [InlineData("FirstnameIsWayTooLong____________________________________________________________________________________________________",
        "Kowalski", "test@test.com", "123456789", 1, "FirstName")] // first name too long
    [InlineData("Jan", "", "test@test.com", "123456789", 1, "LastName")]        // last name empty
    [InlineData("Jan", "LastnameIsWayTooLong____________________________________________________________________________________________________",
        "test@test.com", "123456789", 1, "LastName")] // last name too long
    [InlineData("Jan", "Kowalski", "", "123456789", 1, "Email")]                // email empty
    [InlineData("Jan", "Kowalski", "bad-email", "123456789", 1, "Email")]       // email invalid
    [InlineData("Jan", "Kowalski", "EmailIsWayTooLong_____________________________________________________________________________________________________________________________________________________________________________________@.com",
        "123456789", 1, "Email")] // email too long
    [InlineData("Jan", "Kowalski", "test@test.com", "", 1, "PhoneNumber")]      // phone empty
    [InlineData("Jan", "Kowalski", "test@test.com", "+48 912 0312 4782 43 2531", 1, "PhoneNumber")] // phone too long
    [InlineData("Jan", "Kowalski", "test@test.com", "123456789", 0, "AddressId")] // AddressId invalid
    public void Should_Have_Error_For_Invalid_Input(
        string firstName, string lastName, string email, string phoneNumber, int addressId, string expectedErrorProperty)
    {
        // Arrange
        var model = new UpdateIndividualClientDto
        {
            FirstName = firstName,
            LastName = lastName,
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
        // Arrange
        var model = new UpdateIndividualClientDto
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "valid@test.com",
            PhoneNumber = "123456789",
            AddressId = 1
        };

        // Act
        var result = _validator.TestValidate(model);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}