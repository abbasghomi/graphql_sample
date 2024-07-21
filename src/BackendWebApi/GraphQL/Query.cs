using BackendWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendWebApi.GraphQL;

public class Query
{
    [UseDbContext(typeof(AppDbContext))]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers([ScopedService] AppDbContext context)
    {
        return context.Users.Include(u => u.Scores);
    }
}