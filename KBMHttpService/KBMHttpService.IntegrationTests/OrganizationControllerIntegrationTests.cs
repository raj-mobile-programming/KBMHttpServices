using KBMHttpService.Controllers;
using KBMHttpService.Services;
using KBMGrpcService.Protos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using KBMHttpService.Models;
using KBMGrpcService.Models;

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
            var createRequest = new Models.CreateOrganizationRequestModel { Name = "Test Org", Address = "123 Test St" };
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
            var queryRequest = new QueryRequestModel { page = 1, pageSize = 10 };
            var queryResponse = new QueryOrganizationResponseModel
            {
                Page = 1,
                PageSize = 10,
                Total = 1,
                Organizations = new List<OrganizationsModel>
        {
            new OrganizationsModel { Id = 1, Name = "Test Org", Address = "123 Test St" }
        }
            };

            _organizationServiceMock.Setup(service => service.QueryOrganizationsAsync(queryRequest))
                .ReturnsAsync(queryResponse);

            // Act
            var result = await _controller.QueryOrganizations(queryRequest) as OkObjectResult;

            // Assert
            Assert.NotNull(result);  
            Assert.Equal(200, result.StatusCode); 

            var responseData = result.Value as QueryOrganizationResponseModel;
            Assert.NotNull(responseData);  
            Assert.Equal(1, responseData.Organizations.Count()); 
            Assert.Equal("Test Org", responseData.Organizations.First().Name);  
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
            var updateResponse = new UpdateOrganizationResponse(); 

            _organizationServiceMock.Setup(service => service.UpdateOrganizationAsync(It.IsAny<UpdateOrganizationRequest>()))
                .ReturnsAsync(updateResponse); 

            // Act
            var result = await _controller.UpdateOrganization(updateRequestModel) as OkObjectResult;

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(200, result.StatusCode);  

            var responseData = result.Value as UpdateOrganizationResponse;
            Assert.NotNull(responseData); 
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
            Assert.Equal("Organization deleted successfully.", result.Value);
        }
    }
}
