using KBMGrpcService.Protos;
using KBMHttpService.Controllers;
using KBMHttpService.Models;
using KBMHttpService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class OrganizationControllerTests : IDisposable
{
    private readonly Mock<IOrganizationService> _mockService;
    private readonly OrganizationController _controller;

    public OrganizationControllerTests()
    {
        _mockService = new Mock<IOrganizationService>();
        _controller = new OrganizationController(_mockService.Object);
    }

    [Fact]
    public async Task CreateOrganization_ReturnsOkResult_WithCreatedOrganization()
    {
        // Arrange
        var request = new CreateOrganizationRequestModel { Name = "Test Org", Address = "123 Street" };
        var response = new CreateOrganizationResponseModel { OrganizationId = 1 };

        _mockService.Setup(x => x.CreateOrganizationAsync(It.IsAny<CreateOrganizationRequestModel>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.CreateOrganization(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GetOrganization_ReturnsOkResult_WithOrganization()
    {
        // Arrange
        var response = new GetOrganizationResponseModel { Name = "Test Org", Address = "123 Street" };

        _mockService.Setup(x => x.GetOrganizationAsync(It.IsAny<GetOrganizationRequestModel>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.GetOrganization(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task GetOrganization_ReturnsNotFound_WhenOrganizationNotExists()
    {
        // Arrange
        _mockService.Setup(x => x.GetOrganizationAsync(It.IsAny<GetOrganizationRequestModel>())).ReturnsAsync((GetOrganizationResponseModel)null);

        // Act
        var result = await _controller.GetOrganization(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task QueryOrganizations_ReturnsOkResult_WithOrganizations()
    {
        // Arrange
        var request = new KBMHttpService.Models.QueryRequestModel { page = 1, pageSize = 10 };
        var response = new QueryOrganizationResponseModel();

        _mockService.Setup(x => x.QueryOrganizationsAsync(It.IsAny<QueryRequestModel>()))
                    .ReturnsAsync(response);

        // Act
        var result = await _controller.QueryOrganizations(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(response, okResult.Value);
    }

    [Fact]
    public async Task UpdateOrganization_ReturnsOkResult()
    {
        // Arrange
        var request = new KBMHttpService.Models.UpdateOrganizationRequestModel { OrganizationId = 1, Name = "Updated Org", Address = "456 Street" };

        _mockService.Setup(x => x.UpdateOrganizationAsync(It.IsAny<UpdateOrganizationRequestModel>()));

        // Act
        var result = await _controller.UpdateOrganization(request);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task DeleteOrganization_ReturnsOkResult()
    {
        // Arrange
        _mockService.Setup(x => x.DeleteOrganizationAsync(It.IsAny<DeleteOrganizationRequestModel>()))
                    .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteOrganization(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    public void Dispose()
    {
    }
}
