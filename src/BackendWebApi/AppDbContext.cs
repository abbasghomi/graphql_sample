using BackendWebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendWebApi;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Score> Scores { get; set; }
}