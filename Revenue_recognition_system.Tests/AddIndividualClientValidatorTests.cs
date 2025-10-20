using Revenue_recognition_system.Domain.Constants;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Validators;

namespace Revenue_recognition_system.Tests;
using FluentValidation.TestHelper;
using Xunit;

public class AddIndividualClientValidatorTests
{
    private readonly AddIndividualClientValidator _validator = new();

    // Each InlineData row = one invalid case for a specific property
    [Theory]
    [InlineData("", "Kowalski", "12345678901", "test@test.com", "123456789", 1, "FirstName")]  // first name empty
    [InlineData("FirstnameIsWayTooLong____________________________________________________________________________________________________",
        "Kowalski", "12345678901", "test@test.com", "123456789", 1, "FirstName")] // first name too long
    [InlineData("Jan", "", "12345678901", "test@test.com", "123456789", 1, "LastName")]        // last name empty
    [InlineData("Jan", "LastnameIsWayTooLong____________________________________________________________________________________________________",
        "12345678901", "test@test.com", "123456789", 1, "LastName")] // last name too long
    [InlineData("Jan", "Kowalski", "", "test@test.com", "123456789", 1, "Pesel")]              // PESEL empty
    [InlineData("Jan", "Kowalski", "123", "test@test.com", "123456789", 1, "Pesel")]           // PESEL too short
    [InlineData("Jan", "Kowalski", "1234567890123", "test@test.com", "123456789", 1, "Pesel")] // PESEL too long
    [InlineData("Jan", "Kowalski", "12ab5678901", "test@test.com", "123456789", 1, "Pesel")] // PESEL with non-digit character(s)
    [InlineData("Jan", "Kowalski", "12345678901", "", "123456789", 1, "Email")]                // email empty
    [InlineData("Jan", "Kowalski", "12345678901", "bad-email", "123456789", 1, "Email")]       // email invalid
    [InlineData("Jan", "Kowalski", "12345678901", "EmailIsWayTooLong_____________________________________________________________________________________________________________________________________________________________________________________@.com",
        "123456789", 1, "Email")] // email too long
    [InlineData("Jan", "Kowalski", "12345678901", "test@test.com", "", 1, "PhoneNumber")]      // phone empty
    [InlineData("Jan", "Kowalski", "12345678901", "test@test.com", "+48 912 0312 4782 43 2531", 1, "PhoneNumber")] // phone too long
    [InlineData("Jan", "Kowalski", "12345678901", "test@test.com", "123456789", 0, "AddressId")] // AddressId invalid
    public void Should_Have_Error_For_Invalid_Input(
        string firstName, string lastName, string pesel, string email, string phoneNumber, int addressId, string expectedErrorProperty)
    {
        // Arrange
        var model = new AddIndividualClientDto
        {
            FirstName = firstName,
            LastName = lastName,
            Pesel = pesel,
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
        var model = new AddIndividualClientDto
        {
            FirstName = "Jan",
            LastName = "Kowalski",
            Pesel = new string('1', ClientConstraints.PeselLength), // correct length
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
