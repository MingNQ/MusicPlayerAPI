using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MusicPlayer.Api.Controllers;
using MusicPlayer.Core.Entities.Business;
using MusicPlayer.Core.Interfaces.IServices;
using System.Threading.Tasks;

namespace MusicPlayer.UnitTest;

public class UserApiUnitTest
{
    private readonly Mock<IUserService> _userServiceMock = default!;
    private readonly Mock<ILogger<UserController>> _loggerMock = default!;  
    private readonly UserController _userController = default!;

    public UserApiUnitTest()
    {
        _userServiceMock = new Mock<IUserService>();
        _loggerMock = new Mock<ILogger<UserController>>();
        _userController = new UserController(_userServiceMock.Object, _loggerMock.Object);
    }

    private static UserCreateViewModel SampleCreateModel(string username = "", string email = "", string phoneNumber = "", string password = "", string? profileImageUrl = null, DateTime? createAt = null)
    {
        return new UserCreateViewModel()
        {
            Username = username,
            Email = email,
            PhoneNumber = phoneNumber,
            Password = password,
            ProfileImageUrl = profileImageUrl,
            CreatedAt = createAt ?? DateTime.UtcNow
        };
    }

    private static UserUpdateViewModel SampleUpdateModel(Guid id, string username = "", string email = "", string phoneNumber = "", string? profileImageUrl = null, DateTime? updatedAt = null)
    {
        return new UserUpdateViewModel()
        {
            Id = id.ToString(),
            Username = username,
            Email = email,
            PhoneNumber = phoneNumber,
            ProfileImageUrl = profileImageUrl,
            UpdatedAt = updatedAt ?? DateTime.UtcNow
        };
    }

    [Fact]
    public async Task Create_User_UnitTest()
    {
        #region Create_User_Success

        // Arrange
        var userSample = SampleCreateModel("testuser", "test@example.com");
        var successResponse = new ResponseViewModel
        {
            Success = true,
            Message = "User created successfully.",
        };

        _userServiceMock.Setup(s => s.CreateUserAsync(userSample))
                        .ReturnsAsync(successResponse);

        // Act
        var result = await _userController.CreateUser(userSample);

        // Assert
        var okRequest = Assert.IsType<OkObjectResult>(result);
        var okResponse = Assert.IsAssignableFrom<ResponseViewModel>(okRequest.Value);
        Assert.True(okResponse.Success);

        #endregion

        #region Create_User_Failure

        // Arrange
        userSample = SampleCreateModel("testuser2"); 
        var failureResponse = new ResponseViewModel
        {
            Success = false,
            Message = "User creation failed",
            Error = new ErrorViewModel
            {
                Code = "USER_CREATION_ERROR",
                Message = "User model cannot be null."
            }
        };

        _userServiceMock.Setup(s => s.CreateUserAsync(userSample))
            .ReturnsAsync(failureResponse);

        // Act
        result = await _userController.CreateUser(userSample);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var failReponse = Assert.IsAssignableFrom<ResponseViewModel>(badRequest.Value);
        Assert.False(failReponse.Success);

        #endregion

        #region Create_User_Exception

        // Arrange
        userSample = SampleCreateModel("testuser3");

        _userServiceMock.Setup(s => s.CreateUserAsync(userSample))
            .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        result = await _userController.CreateUser(userSample);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        var exceptionResponse = Assert.IsAssignableFrom<ResponseViewModel>(statusResult.Value);
        Assert.False(exceptionResponse.Success);
        Assert.Equal("An error occurred while creating user.", exceptionResponse.Message);

        #endregion
    }

    [Fact]
    public async Task Update_User_UnitTest()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userUpdateModel = SampleUpdateModel(userId, "testUser");
        var successResponse = new ResponseViewModel
        {
            Success = true,
            Message = "User updated successfully."
        };

        _userServiceMock.Setup(s => s.UpdateUserAsync(userUpdateModel))
                        .ReturnsAsync(successResponse);

        // Act
        var result = await _userController.UpdateUser(userId, userUpdateModel);
        
        // Assert
        var okRequest = Assert.IsType<OkObjectResult>(result);
        var okResponse = Assert.IsAssignableFrom<ResponseViewModel>(okRequest.Value);
        Assert.True(okResponse.Success);
    }
    [Fact]
    public async Task Delete_User_UnitTest()
    {
        #region Delete_User_Success

        // Arrange
        var userId = Guid.NewGuid();
        var successResponse = new ResponseViewModel
        {
            Success = true,
            Message = "User deleted successfully."
        };

        _userServiceMock.Setup(s => s.DeleteUserAsync(userId))
                        .Returns(Task.FromResult(successResponse));

        // Act
        var result = await _userController.DeleteUser(userId);

        // Assert
        var okRequest = Assert.IsType<OkObjectResult>(result);
        var okResponse = Assert.IsAssignableFrom<ResponseViewModel>(okRequest.Value);
        Assert.True(okResponse.Success);

        #endregion

        #region Delete_User_Exception

        // Arrange
        _userServiceMock.Setup(s => s.DeleteUserAsync(userId))
                        .ThrowsAsync(new Exception("Unexpected error"));

        // Act
        result = await _userController.DeleteUser(userId);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
        var exceptionResponse = Assert.IsAssignableFrom<ResponseViewModel>(statusResult.Value);
        Assert.False(exceptionResponse.Success);
        Assert.Equal("An error occurred while deleting user.", exceptionResponse.Message);

        #endregion
    }
}