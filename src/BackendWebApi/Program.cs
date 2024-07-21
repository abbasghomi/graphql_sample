using Microsoft.EntityFrameworkCore;
using BackendWebApi.Data;
using BackendWebApi.GraphQL;
using BackendWebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(options =>
    options.UseInMemoryDatabase("GraphQLDb"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddType<UserType>()
    .AddType<ScoreType>()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

app.MapGraphQL();
app.MapBananaCakePop("/graphql-ui"); // Enable Banana Cake Pop GraphQL IDE

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DataSeeder.SeedData(services);
}

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }