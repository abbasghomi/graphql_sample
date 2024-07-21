using FluentAssertions;
using System.Text;
using System.Text.Json;

namespace BackendWebApi.Test
{
    public class GraphQLIntegrationTests 
    {

        [Fact]
        public async Task Query_Users_ReturnsExpectedData()
        {
            await using var application = new BackendWebApiApplication();
            using var _client = application.CreateClient();

            // Arrange
            var query = new
            {
                query = "{ users { id name } }"
            };
            var content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/graphql", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("\"name\":\"John Doe\"");
        }
    }
}
