using KBMHttpService.Controllers;
using KBMHttpService.Services;
using KBMGrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KBMHttpService.Tests.Integration
{
    public class OrganizationControllerTests
    {
        private readonly Mock<IOrganizationService> _organizationServiceMock;
        private readonly OrganizationController _controller;

        public OrganizationControllerTests()
        {
            _organizationServiceMock = new Mock<IOrganizationService>();
            _controller = new OrganizationController(_organizationServiceMock.Object);
        }

        [Fact]
        public async Task CreateOrganization_ReturnsSuccess()
        {
            // Arrange
            var createRequest = new CreateOrganizationRequest { Name = "Test Org", Address = "123 Test St" };
            var createResponse = new CreateOrganizationResponse { OrganizationId = 1 };

            _organizationServiceMock.Setup(service => service.CreateOrganizationAsync(createRequest))
                .ReturnsAsync(createResponse);

            // Act
            var result = await _controller.CreateOrganization(createRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var responseData = result.Value as CreateOrganizationResponse;
            Assert.NotNull(responseData);
            Assert.Equal(1, responseData.OrganizationId);
        }

        [Fact]
        public async Task GetOrganization_ReturnsOrganization_WhenFound()
        {
            // Arrange
            var organizationId = 1L;
            var getRequest = new GetOrganizationRequest { Id = organizationId };
            var getResponse = new GetOrganizationResponse { Name = "Test Org", Address = "123 Test St" };

            _organizationServiceMock.Setup(service => service.GetOrganizationAsync(getRequest))
                .ReturnsAsync(getResponse);

            // Act
            var result = await _controller.GetOrganization(organizationId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var responseData = result.Value as GetOrganizationResponse;
            Assert.NotNull(responseData);
            Assert.Equal(organizationId, 1);
        }

        [Fact]
        public async Task GetOrganization_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            var organizationId = 999L;
            var getRequest = new GetOrganizationRequest { Id = organizationId };

            _organizationServiceMock.Setup(service => service.GetOrganizationAsync(getRequest))
                .ReturnsAsync((GetOrganizationResponse)null);

            // Act
            var result = await _controller.GetOrganization(organizationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task QueryOrganizations_ReturnsOrganizations()
        {
            // Arrange
            var queryRequest = new QueryOrganizationsRequest { Page = 1, PageSize = 10 };
            var queryResponse = new QueryOrganizationsResponse
            {
                Page = 1,
                PageSize = 10,
                Organizations = { new OrganizationModel { Id = 1, Name = "Test Org", Address = "123 Test St" } },
                Total = 1
            };

            _organizationServiceMock.Setup(service => service.QueryOrganizationsAsync(queryRequest))
                .ReturnsAsync(queryResponse);

            // Act
            var result = await _controller.QueryOrganizations(queryRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var responseData = result.Value as QueryOrganizationsResponse;
            Assert.NotNull(responseData);
            Assert.NotEmpty(responseData.Organizations);
        }

        [Fact]
        public async Task UpdateOrganization_ReturnsSuccess()
        {
            // Arrange
            var updateRequestModel = new KBMHttpService.Models.UpdateOrganizationRequestModel
            {
                OrganizationId = 1,
                Name = "Updated Org",
                Address = "456 Updated St"
            };
            var updateRequest = new UpdateOrganizationRequest
            {
                OrganizationId = 1,
                Name = "Updated Org",
                Address = "456 Updated St"
            };

            _organizationServiceMock.Setup(service => service.UpdateOrganizationAsync(updateRequest))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateOrganization(updateRequestModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Success", result.Value);
        }

        [Fact]
        public async Task DeleteOrganization_ReturnsSuccess()
        {
            // Arrange
            var organizationId = 1L;
            var deleteRequest = new DeleteOrganizationRequest { OrganizationId = organizationId };

            _organizationServiceMock.Setup(service => service.DeleteOrganizationAsync(deleteRequest))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteOrganization(organizationId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Success", result.Value);
        }
    }
}
