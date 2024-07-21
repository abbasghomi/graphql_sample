using GraphQL;
using GraphQL.Client.Http;

namespace FrontendConsoleApp.Services
{
    public class DataService
    {
        public async Task FetchAllUsers(GraphQLHttpClient client)
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

            Console.WriteLine("All Users:");
            foreach (var user in response.Data.Users)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Display Name: {user.DisplayName}");
                foreach (var score in user.Scores)
                {
                    Console.WriteLine($"    Course: {score.Course}, Score: {score.Value}");
                }
            }
        }

        public async Task FetchFilteredUsersAsync(GraphQLHttpClient client, string emailDomain)
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

            var response = await client.SendQueryAsync<ResponseData>(request);

            Console.WriteLine($"\nUsers with email ending in '{emailDomain}':");
            foreach (var user in response.Data.Users)
            {
                Console.WriteLine($"Id: {user.Id}, Name: {user.Name}, Email: {user.Email}, Display Name: {user.DisplayName}");
                foreach (var score in user.Scores)
                {
                    Console.WriteLine($"    Course: {score.Course}, Score: {score.Value}");
                }
            }
        }
    }
}
