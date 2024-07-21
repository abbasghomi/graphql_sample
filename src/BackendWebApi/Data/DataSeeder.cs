using BackendWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendWebApi.Data;

public static class DataSeeder
{
    public static void SeedData(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (!context.Users.Any())
        {
            var users = new[]
            {
                new User { Name = "John Doe", Email = "john@example.com" },
                new User { Name = "Jane Smith", Email = "jane@example.com" },
                new User { Name = "Bob Johnson", Email = "bob@example.com" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var scores = new[]
            {
                new Score { UserId = users[0].Id, Course = "Math", Value = 95 },
                new Score { UserId = users[0].Id, Course = "Science", Value = 88 },
                new Score { UserId = users[1].Id, Course = "Math", Value = 76 },
                new Score { UserId = users[1].Id, Course = "Science", Value = 92 },
                new Score { UserId = users[2].Id, Course = "Math", Value = 65 },
                new Score { UserId = users[2].Id, Course = "Science", Value = 73 }
            };

            context.Scores.AddRange(scores);
            context.SaveChanges();
        }
    }
}