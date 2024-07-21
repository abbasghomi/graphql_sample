using GraphQL;
using GraphQL.Client.Http;

namespace FrontendConsoleApp.Test.Contracts;

public interface IGraphQLClient
{
    Task<GraphQLResponse<TResponse>> SendQueryAsync<TResponse>(GraphQLRequest request, CancellationToken cancellationToken = default);
   
}