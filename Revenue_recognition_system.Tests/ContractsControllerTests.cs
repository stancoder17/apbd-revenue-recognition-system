using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Revenue_recognition_system.API.Controllers;
using Revenue_recognition_system.Services.DTOs;
using Revenue_recognition_system.Services.Interfaces;

namespace Revenue_recognition_system.Tests;

public class ContractsControllerTests
{
    private static ContractsController CreateController(Mock<IContractService> mock)
        => new ContractsController(mock.Object);

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
        var mock = new Mock<IContractService>();
        mock.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((GetContractDto?)null);
        var controller = CreateController(mock);

        var result = await controller.GetById(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
