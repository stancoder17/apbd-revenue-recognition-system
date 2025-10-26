using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revenue_recognition_system.API.Controllers;
using Revenue_recognition_system.Domain.Exceptions;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Tests;

public class ContractControllerTests
{
    private static ContractController CreateController(Mock<IContractService> mock)
        => new ContractController(mock.Object);

    [Fact]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        // Arrange
        var mock = new Mock<IContractService>();
        var dto = new GetContractDto { Id = 1, ClientId = 10, SoftwareVersionId = 100, StartDate = DateTime.UtcNow.Date, EndDate = DateTime.UtcNow.Date.AddDays(30), FinalPrice = 123.45m, AdditionalSupportYears = 1, SignedAt = DateTime.UtcNow.Date };
        mock.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(dto);
        var controller = CreateController(mock);

        // Act
        var result = await controller.GetById(1);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        Assert.Same(dto, ok.Value);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var mock = new Mock<IContractService>();
        mock.Setup(s => s.GetByIdAsync(999)).ThrowsAsync(new NotFoundException("not found"));
        var controller = CreateController(mock);

        // Act
        var result = await controller.GetById(999);

        // Assert
        var nf = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("not found", nf.Value);
    }

    [Fact]
    public async Task Add_ReturnsCreatedAt_WithDtoAndRoute()
    {
        // Arrange
        var mock = new Mock<IContractService>();
        var add = new AddContractDto { ClientId = 7, SoftwareVersionId = 3, EndDate = DateTime.UtcNow.Date.AddDays(10), AdditionalSupportYears = 2 };
        var created = new GetContractDto { Id = 42, ClientId = add.ClientId, SoftwareVersionId = add.SoftwareVersionId, StartDate = DateTime.UtcNow.Date, EndDate = add.EndDate, FinalPrice = 9999.99m, AdditionalSupportYears = add.AdditionalSupportYears };
        mock.Setup(s => s.AddAsync(add)).ReturnsAsync(created);
        var controller = CreateController(mock);

        // Act
        var result = await controller.Add(add);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdAt.StatusCode);
        Assert.Equal(nameof(ContractController.GetById), createdAt.ActionName);
        Assert.Equal(created, createdAt.Value);
        Assert.NotNull(createdAt.RouteValues);
        Assert.Equal(created.Id, createdAt.RouteValues!["contractId"]);
    }

    [Fact]
    public async Task Add_ReturnsNotFound_OnServiceException()
    {
        var mock = new Mock<IContractService>();
        var add = new AddContractDto { ClientId = 7, SoftwareVersionId = -999, EndDate = DateTime.UtcNow.Date.AddDays(10), AdditionalSupportYears = 2 };
        mock.Setup(s => s.AddAsync(add)).ThrowsAsync(new NotFoundException("SoftwareVersion missing"));
        var controller = CreateController(mock);

        var result = await controller.Add(add);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
        Assert.Equal("SoftwareVersion missing", notFound.Value);
    }

    [Fact]
    public async Task Add_ReturnsBadRequest_OnServiceException()
    {
        var mock = new Mock<IContractService>();
        var add = new AddContractDto { ClientId = 7, SoftwareVersionId = 3, EndDate = DateTime.UtcNow.Date.AddDays(1), AdditionalSupportYears = 5 };
        mock.Setup(s => s.AddAsync(add)).ThrowsAsync(new BadRequestException("validation failed"));
        var controller = CreateController(mock);

        var result = await controller.Add(add);

        var bad = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, bad.StatusCode);
        Assert.Equal("validation failed", bad.Value);
    }
}
