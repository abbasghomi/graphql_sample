using FrontendConsoleApp.Test.Contracts;
using FrontendConsoleApp.Test.Services;
using GraphQL;
using Moq;

namespace FrontendConsoleApp.Test;

public class DataServiceTests
{
    private readonly Mock<IGraphQLClient> _mockClient;
    private readonly MockedDataService _mockedDataService;

    public DataServiceTests()
    {
        _mockClient = new Mock<IGraphQLClient>();

        _mockedDataService = new MockedDataService();
    }

    [Fact]
    public async Task FetchAllUsers_ShouldReturnResult()
    {
        // Arrange
        var query = new GraphQLRequest
        {
            Query = @"
                {
                    users {
                        id
                        name
                        email
                        displayName
                        scores {
                            course
                            value
                        }
                    }
                }"
        };

        var mockResponse = new GraphQLResponse<ResponseData>
        {
            Data = new ResponseData
            {
                Users = new[]
                {
                        new User
                        {
                            Id = 1,
                            Name = "John Doe",
                            Email = "john@example.com",
                            DisplayName = "John Doe (john@example.com)",
                            Scores = new[]
                            {
                                new Score { Course = "Math", Value = 95 },
                                new Score { Course = "Science", Value = 88 }
                            }
                        }
                    }
            }
        };

        _mockClient.Setup(c => c.SendQueryAsync<ResponseData>(It.IsAny<GraphQLRequest>(), default))
            .ReturnsAsync(mockResponse);

        // Act
        await _mockedDataService.FetchAllUsers(_mockClient.Object);

        // Assert
        _mockClient.Verify(c => c.SendQueryAsync<ResponseData>(It.IsAny<GraphQLRequest>(), default), Times.Once);
    }

    private string ExtractEmailDomain(GraphQLRequest request)
    {
        return ((dynamic)request.Variables).emailDomain;
    }

    [Fact]
    public async Task FetchAllUsers_ProcessesResponseCorrectly()
    {
        //// Arrange
        var query = new GraphQLRequest
        {
            Query = @"
                {
                    users {
                        id
                        name
                        email
                        displayName
                        scores {
                            course
                            value
                        }
                    }
                }"
        };

        var mockResponse = new GraphQLResponse<ResponseData>
        {
            Data = new ResponseData
            {
                Users = new[]
                {
                    new User
                    {
                        Id = 1,
                        Name = "John Doe",
                        Email = "john@example.com",
                        DisplayName = "John Doe (john@example.com)",
                        Scores = new[]
                        {
                            new Score { Course = "Math", Value = 95 },
                            new Score { Course = "Science", Value = 88 }
                        }
                    }
                }
            }
        };

        _mockClient.Setup(c => c.SendQueryAsync<ResponseData>(It.IsAny<GraphQLRequest>(), default))
            .ReturnsAsync(mockResponse);

        //Act
        var service = new MockedDataService();

        var response = await service.FetchAllUsers(_mockClient.Object);

        ////Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Data);
        Assert.NotNull(response.Data.Users);
        Assert.Single(response.Data.Users);
        var user = response.Data.Users.First();
        Assert.Equal("John Doe", user.Name);
        Assert.Equal("john@example.com", user.Email);
        Assert.Equal("John Doe (john@example.com)", user.DisplayName);
        Assert.Equal(2, user.Scores.Length);
        Assert.Contains(user.Scores, s => s.Course == "Math" && s.Value == 95);
        Assert.Contains(user.Scores, s => s.Course == "Science" && s.Value == 88);
    }

}