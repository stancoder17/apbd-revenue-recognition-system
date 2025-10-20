using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revenue_recognition_system.API.Controllers;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Tests;

public class ClientsControllerTests
{
    private static ClientsController CreateController(Mock<IClientService> mock)
        => new ClientsController(mock.Object);

    [Fact]
    public async Task GetIndividualById_ReturnsOk_WithDto()
    {
        // Arrange
        var mock = new Mock<IClientService>();
        var dto = new GetIndividualClientDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Pesel = "12345678901",
            Email = "john.doe@example.com",
            PhoneNumber = "+48123456789",
            Address = new GetAddressDto { Id = 10, City = "City", Street = "Street", Number = "1", PostalCode = "00-000" }
        };
        mock.Setup(s => s.GetIndividualByIdAsync(1)).ReturnsAsync(dto);
        var controller = CreateController(mock);

        // Act
        var result = await controller.GetIndividualById(1);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(dto, ok.Value);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
    }

    [Fact]
    public async Task GetIndividualById_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(s => s.GetIndividualByIdAsync(99)).ThrowsAsync(new NotFoundException("not found"));
        var controller = CreateController(mock);

        var result = await controller.GetIndividualById(99);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
        Assert.Equal("not found", notFound.Value);
    }

    [Fact]
    public async Task AddIndividual_ReturnsCreatedAt_WithDtoAndRoute()
    {
        // Arrange
        var mock = new Mock<IClientService>();
        var addDto = new AddIndividualClientDto
        {
            FirstName = "Jane",
            LastName = "Roe",
            Pesel = "11122233344",
            Email = "jane.roe@example.com",
            PhoneNumber = "+48111222333",
            AddressId = 20
        };
        var created = new GetIndividualClientDto
        {
            Id = 5,
            FirstName = "Jane",
            LastName = "Roe",
            Pesel = "11122233344",
            Email = "jane.roe@example.com",
            PhoneNumber = "+48111222333",
            Address = new GetAddressDto { Id = 20, City = "Town", Street = "Main", Number = "2A", PostalCode = "11-111" }
        };
        mock.Setup(s => s.AddIndividualAsync(addDto)).ReturnsAsync(created);
        var controller = CreateController(mock);

        // Act
        var result = await controller.AddIndividual(addDto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ClientsController.GetIndividualById), createdAt.ActionName);
        Assert.Equal(StatusCodes.Status201Created, createdAt.StatusCode);
        Assert.Equal(created, createdAt.Value);
        Assert.NotNull(createdAt.RouteValues);
        Assert.True(createdAt.RouteValues!.ContainsKey("clientId"));
        Assert.Equal(created.Id, createdAt.RouteValues["clientId"]);
    }

    [Fact]
    public async Task AddIndividual_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        var addDto = new AddIndividualClientDto { FirstName = "A", LastName = "B", Pesel = "12345678901", Email = "a@b.com", PhoneNumber = "+48123456789", AddressId = 999 };
        mock.Setup(s => s.AddIndividualAsync(addDto)).ThrowsAsync(new NotFoundException("Address doesn't exist."));
        var controller = CreateController(mock);

        var result = await controller.AddIndividual(addDto);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Address doesn't exist.", notFound.Value);
    }

    [Fact]
    public async Task AddIndividual_BadRequest_OnAlreadyExists()
    {
        var mock = new Mock<IClientService>();
        var addDto = new AddIndividualClientDto { FirstName = "A", LastName = "B", Pesel = "12345678901", Email = "a@b.com", PhoneNumber = "+48123456789", AddressId = 1 };
        mock.Setup(s => s.AddIndividualAsync(addDto)).ThrowsAsync(new AlreadyExistsException("already exists"));
        var controller = CreateController(mock);

        var result = await controller.AddIndividual(addDto);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, bad.StatusCode);
        Assert.Equal("already exists", bad.Value);
    }

    [Fact]
    public async Task DeleteIndividual_NoContent_OnSuccess()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(s => s.DeleteIndividualAsync(1)).Returns(Task.CompletedTask);
        var controller = CreateController(mock);

        var result = await controller.DeleteIndividual(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteIndividual_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(s => s.DeleteIndividualAsync(7)).ThrowsAsync(new NotFoundException("client not found"));
        var controller = CreateController(mock);

        var result = await controller.DeleteIndividual(7);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("client not found", notFound.Value);
    }

    [Fact]
    public async Task DeleteIndividual_BadRequest_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(s => s.DeleteIndividualAsync(7)).ThrowsAsync(new BadRequestException("bad"));
        var controller = CreateController(mock);

        var result = await controller.DeleteIndividual(7);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("bad", bad.Value);
    }

    [Fact]
    public async Task GetCompanyById_ReturnsOk_WithDto()
    {
        var mock = new Mock<IClientService>();
        var dto = new GetCompanyClientDto
        {
            Id = 2,
            Name = "Acme",
            Krs = "0000123456",
            Email = "contact@acme.com",
            PhoneNumber = "+48123456780",
            Address = new GetAddressDto { Id = 30, City = "City", Street = "Street", Number = "3", PostalCode = "22-222" }
        };
        mock.Setup(s => s.GetCompanyByIdAsync(2)).ReturnsAsync(dto);
        var controller = CreateController(mock);

        var result = await controller.GetCompanyById(2);

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Same(dto, ok.Value);
    }

    [Fact]
    public async Task GetCompanyById_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        mock.Setup(s => s.GetCompanyByIdAsync(3)).ThrowsAsync(new NotFoundException("no company"));
        var controller = CreateController(mock);

        var result = await controller.GetCompanyById(3);

        var nf = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("no company", nf.Value);
    }

    [Fact]
    public async Task AddCompany_ReturnsCreatedAt_WithDtoAndRoute()
    {
        var mock = new Mock<IClientService>();
        var addDto = new AddCompanyClientDto
        {
            Name = "Globex",
            Krs = "0000654321",
            Email = "office@globex.com",
            PhoneNumber = "+48987654321",
            AddressId = 44
        };
        var created = new GetCompanyClientDto
        {
            Id = 12,
            Name = "Globex",
            Krs = "0000654321",
            Email = "office@globex.com",
            PhoneNumber = "+48987654321",
            Address = new GetAddressDto { Id = 44, City = "Metro", Street = "Ave", Number = "10", PostalCode = "33-333" }
        };
        mock.Setup(s => s.AddCompanyAsync(addDto)).ReturnsAsync(created);
        var controller = CreateController(mock);

        var result = await controller.AddCompany(addDto);

        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ClientsController.GetCompanyById), createdAt.ActionName);
        Assert.Equal(created, createdAt.Value);
        Assert.NotNull(createdAt.RouteValues);
        Assert.Equal(created.Id, createdAt.RouteValues!["clientId"]);
    }

    [Fact]
    public async Task AddCompany_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        var addDto = new AddCompanyClientDto { Name = "X", Krs = "123", Email = "x@x.com", PhoneNumber = "+481", AddressId = 1 };
        mock.Setup(s => s.AddCompanyAsync(addDto)).ThrowsAsync(new NotFoundException("addr"));
        var controller = CreateController(mock);

        var result = await controller.AddCompany(addDto);

        var nf = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("addr", nf.Value);
    }

    [Fact]
    public async Task AddCompany_BadRequest_OnAlreadyExists()
    {
        var mock = new Mock<IClientService>();
        var addDto = new AddCompanyClientDto { Name = "Y", Krs = "krs", Email = "y@y.com", PhoneNumber = "+482", AddressId = 2 };
        mock.Setup(s => s.AddCompanyAsync(addDto)).ThrowsAsync(new AlreadyExistsException("exists"));
        var controller = CreateController(mock);

        var result = await controller.AddCompany(addDto);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("exists", bad.Value);
    }

    [Fact]
    public async Task UpdateIndividual_ReturnsOk_WithDto()
    {
        // Arrange
        var mock = new Mock<IClientService>();
        var updateDto = new UpdateIndividualClientDto
        {
            FirstName = "Anna",
            LastName = "Doe",
            Email = "anna.doe@example.com",
            PhoneNumber = "+48123456788",
            AddressId = 10
        };
        var updated = new GetIndividualClientDto
        {
            Id = 1,
            FirstName = "Anna",
            LastName = "Doe",
            Pesel = "12345678901",
            Email = "anna.doe@example.com",
            PhoneNumber = "+48123456788",
            Address = new GetAddressDto { Id = 10, City = "City", Street = "Street", Number = "1", PostalCode = "00-000" }
        };
        mock.Setup(s => s.UpdateIndividualAsync(1, updateDto)).ReturnsAsync(updated);
        var controller = CreateController(mock);

        // Act
        var result = await controller.UpdateIndividual(1, updateDto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        Assert.Same(updated, ok.Value);
    }

    [Fact]
    public async Task UpdateIndividual_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        var updateDto = new UpdateIndividualClientDto { FirstName = "X", LastName = "Y", Email = "x@y.com", PhoneNumber = "+480", AddressId = 999 };
        mock.Setup(s => s.UpdateIndividualAsync(42, updateDto)).ThrowsAsync(new NotFoundException("not found"));
        var controller = CreateController(mock);

        var result = await controller.UpdateIndividual(42, updateDto);

        var nf = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, nf.StatusCode);
        Assert.Equal("not found", nf.Value);
    }

    [Fact]
    public async Task UpdateCompany_ReturnsOk_WithDto()
    {
        // Arrange
        var mock = new Mock<IClientService>();
        var updateDto = new UpdateCompanyClientDto
        {
            Name = "Acme Ltd.",
            Email = "new@acme.com",
            PhoneNumber = "+48123456781",
            AddressId = 30
        };
        var updated = new GetCompanyClientDto
        {
            Id = 2,
            Name = "Acme Ltd.",
            Krs = "0000123456",
            Email = "new@acme.com",
            PhoneNumber = "+48123456781",
            Address = new GetAddressDto { Id = 30, City = "City", Street = "Street", Number = "3", PostalCode = "22-222" }
        };
        mock.Setup(s => s.UpdateCompanyAsync(2, updateDto)).ReturnsAsync(updated);
        var controller = CreateController(mock);

        // Act
        var result = await controller.UpdateCompany(2, updateDto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        Assert.Same(updated, ok.Value);
    }

    [Fact]
    public async Task UpdateCompany_NotFound_OnServiceException()
    {
        var mock = new Mock<IClientService>();
        var updateDto = new UpdateCompanyClientDto { Name = "Z", Email = "z@z.com", PhoneNumber = "+483", AddressId = 1 };
        mock.Setup(s => s.UpdateCompanyAsync(77, updateDto)).ThrowsAsync(new NotFoundException("no company"));
        var controller = CreateController(mock);

        var result = await controller.UpdateCompany(77, updateDto);

        var nf = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, nf.StatusCode);
        Assert.Equal("no company", nf.Value);
    }
}
