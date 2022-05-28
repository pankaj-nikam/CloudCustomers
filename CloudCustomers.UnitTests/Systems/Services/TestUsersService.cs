using CloudCustomers.Models;
using CloudCustomers.Models.Configurations;
using CloudCustomers.Services.Concrete;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handler.Object);
            var endPoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            //Act
            await sut.GetAllUsers();
            //Assert
            //Verify HTTP request was made.
            handler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                );
        }

        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsListOfUsers()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handler = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handler.Object);
            var endPoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsEmptyListOfUsersOfExpectedSize()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var handler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handler.Object);
            var endPoint = "https://example.com";
            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endPoint
            });
            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            result.Count.Should().Be(result.Count);
        }

        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesExternalUrl()
        {
            //Arrange
            var expectedResponse = UsersFixture.GetTestUsers();
            var endPoint = "https://abc.com/users";
            var handler = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endPoint);
            var httpClient = new HttpClient(handler.Object);

            var config = Options.Create(new UsersApiOptions
            {
                Endpoint = endPoint
            });

            var sut = new UsersService(httpClient, config);
            //Act
            var result = await sut.GetAllUsers();
            //Assert
            //Verify HTTP request was made.
            handler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get && req.RequestUri.ToString() == endPoint),
                    ItExpr.IsAny<CancellationToken>()
                );
        }
    }
}