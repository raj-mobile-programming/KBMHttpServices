using Microsoft.AspNetCore.Mvc;
using Moq;
using KBMHttpService.Controllers;
using KBMHttpService.Services;
using KBMHttpService.Models;
using KBMGrpcService.Models;
using KBMGrpcService.Protos;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResult()
    {
        // Arrange
        var request = new CreateUserRequestModel
        {
            Name = "Test Name",
            Username = "testusername",
            Email = "test@example.com"
        };

        var response = new CreateUserResponseModel { UserId = 1 };
        _mockUserService.Setup(service => service.CreateUserAsync(It.IsAny<CreateUserRequestModel>()))
                        .ReturnsAsync(response);

        // Act
        var result = await _controller.CreateUser(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CreateUserResponseModel>(okResult.Value);
        Assert.Equal(1, returnValue.UserId);
    }

    [Fact]
    public async Task GetUser_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var response = new GetUserResponse
        {
            Name = "Test Name",
            Username = "testusername",
            Email = "test@example.com",
            CreatedAt = 1724615233
        };

        _mockUserService.Setup(service => service.GetUserAsync(It.IsAny<GetUserRequest>()))
                        .ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GetUserResponse>(okResult.Value);
        Assert.Equal("Test Name", returnValue.Name);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockUserService.Setup(service => service.GetUserAsync(It.IsAny<GetUserRequest>()))
                        .ReturnsAsync((GetUserResponse)null);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task QueryUsers_ReturnsOkResult()
    {
        // Arrange
        var response = new QueryUsersResponseModel
        {
            Page = 1,
            PageSize = 10,
            Users = new List<UsersModel>
        {
            new UsersModel
            {
                Id = 1,
                Name = "Test Name",
                Username = "testusername",
                Email = "test@example.com",
                CreatedAt = 1724612047
            }
        }
        };

        _mockUserService.Setup(service => service.QueryUsersAsync(It.IsAny<QueryUserRequestModel>()))
                        .ReturnsAsync(response);

        var request = new QueryOrganizationRequestModel
        {
            page = 1,
            pageSize = 10,
            orderBy = "Name",
            direction = "ASC",
            queryString = "Test"
        };

        // Act
        var result = await _controller.QueryUsers(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<QueryUsersResponseModel>(okResult.Value);
        Assert.Equal(1, returnValue.Users.Count());
    }

    [Fact]
    public async Task UpdateUser_ReturnsOkResult()
    {
        // Arrange
        var request = new UpdateUserRequestModel
        {
            UserId = 1,
            Name = "Updated Name",
            Username = "updatedusername",
            Email = "updated@example.com"
        };

        _mockUserService.Setup(service => service.UpdateUserAsync(It.IsAny<UpdateUserRequestModel>()))
                        .ReturnsAsync(new UpdateUserResponseModel { Message = "User updated successfully." });

        // Act
        var result = await _controller.UpdateUser(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Success", returnValue);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOkResult()
    {
        // Arrange
        var userId = 1;

        _mockUserService.Setup(service => service.DeleteUserAsync(It.IsAny<DeleteUserRequestModel>()))
                        .ReturnsAsync(new DeleteUserResponseModel { Message = "User deleted successfully." });

        // Act
        var result = await _controller.DeleteUser(userId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Success", returnValue);
    }

    [Fact]
    public async Task AssociateUserToOrganization_ReturnsOkResult()
    {
        // Arrange
        var request = new AssociateUserRequestModel
        {
            UserId = 1,
            OrganizationId = 100
        };

        _mockUserService.Setup(service => service.AssociateUserToOrganizationAsync(It.IsAny<AssociateUserRequestModel>()))
                        .ReturnsAsync("User associated successfully");

        // Act
        var result = await _controller.AssociateUserToOrganization(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Success", returnValue);
    }
    [Fact]
    public async Task DisassociateUserFromOrganization_ReturnsOkResult()
    {
        // Arrange
        var request = new DisassociateUserRequestModel
        {
            UsertId = 1
        };

        _mockUserService.Setup(service => service.DisassociateUserFromOrganizationAsync(It.IsAny<DisassociateUserRequestModel>()))
                        .ReturnsAsync("User disassociated successfully");

        // Act
        var result = await _controller.DisassociateUserFromOrganizationRequest(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<string>(okResult.Value);
        Assert.Equal("Success", returnValue);
    }
    [Fact]
    public async Task GetUserReturnsNotFoundWhenUserDoesNotExist()
    {
        // Arrange
        _mockUserService.Setup(service => service.GetUserAsync(It.IsAny<GetUserRequest>()))
                        .ReturnsAsync((GetUserResponse)null);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

}
