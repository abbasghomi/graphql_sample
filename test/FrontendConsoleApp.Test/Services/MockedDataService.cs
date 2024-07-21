using FrontendConsoleApp.Test.Contracts;
using GraphQL;
using GraphQL.Client.Http;

namespace FrontendConsoleApp.Test.Services;

public class MockedDataService
{
    public async Task<GraphQLResponse<ResponseData>> FetchAllUsers(IGraphQLClient client)
    {
        var request = new GraphQLRequest
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

        var response = await client.SendQueryAsync<ResponseData>(request);

        return response;

    }

    public GraphQLResponse<ResponseData> FetchFilteredUsers(IGraphQLClient client, string emailDomain)
    {
        var request = new GraphQLRequest
        {
            Query = @"
                query($emailDomain: String) {
                    users(where: {email: {endsWith: $emailDomain}}) {
                        id
                        name
                        email
                        displayName
                        scores {
                            course
                            value
                        }
                    }
                }",
            Variables = new { emailDomain }
        };

        var response = new GraphQLResponse<ResponseData>
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

        return response;
    }
}