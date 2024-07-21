// See https://aka.ms/new-console-template for more information

using FrontendConsoleApp.Services;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

Console.WriteLine("GraphQL Frontend sample");

var service = new DataService();

var client = new GraphQLHttpClient("http://localhost:5000/graphql", new NewtonsoftJsonSerializer());

await service.FetchAllUsers(client);
await service.FetchFilteredUsersAsync(client, "example.com");

Console.ReadKey();

public partial class Program { }