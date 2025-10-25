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
}
