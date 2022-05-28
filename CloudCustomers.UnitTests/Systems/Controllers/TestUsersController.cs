using System;
using System.Threading.Tasks;
using CloudCustomers.API.Controllers;
using CloudCustomers.Models;
using CloudCustomers.Services.Abstract;
using CloudCustomers.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CloudCustomers.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = (OkObjectResult) await sut.Get();
        //Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserService()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        mockUserService.Verify(service => service.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async Task Get_OnNoUsersFoundReturns404()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);
        //Act
        var result = await sut.Get();
        //Assert
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Get_OnNoUsersFound_ReturnsStatusCode400()
    {
        //Arrange
        var mockUserService = new Mock<IUsersService>();
        
        var sut = new UsersController(mockUserService.Object);
        mockUserService
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        //Act
        var result =await sut.Get();
        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        //Assert
        objectResult.StatusCode.Should().Be(404);
    }
}