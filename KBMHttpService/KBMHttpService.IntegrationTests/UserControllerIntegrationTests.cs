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
    private readonly HttpClient _client;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _controller = new UserController(_mockUserService.Object);
    }

    [Fact]
    public async Task GetUser_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var response = new GetUserResponseModel
        {
            Name = "Test Name",
            Username = "testusername",
            Email = "test@example.com",
            CreatedAt = 1724612047
        };

        _mockUserService.Setup(service => service.GetUserAsync(It.IsAny<GetUserRequestModel>())).ReturnsAsync(response);

        // Act
        var result = await _controller.GetUser(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<GetUserResponseModel>(okResult.Value);
        Assert.Equal("Test Name", returnValue.Name);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockUserService.Setup(service => service.GetUserAsync(It.IsAny<GetUserRequestModel>()));

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

        _mockUserService.Setup(service => service.QueryUsersAsync(It.IsAny<QueryRequestModel>()))
                        .ReturnsAsync(response);

        var request = new QueryRequestModel
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
}
