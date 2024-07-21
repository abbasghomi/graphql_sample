using BackendWebApi.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BackendWebApi.Test
{
    internal class BackendWebApiApplication : WebApplicationFactory<Program>
    {
        private readonly string _environment;

        public BackendWebApiApplication(string environment = "Development")
        {
            _environment = environment;
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                //services.AddScoped(sp =>
                //{
                //    // Replace SQLite with in-memory database for tests
                //    return new DbContextOptionsBuilder<TodoDb>()
                //        .UseInMemoryDatabase("Tests")
                //        .UseApplicationServiceProvider(sp)
                //        .Options;
                //});



                builder.ConfigureServices(services =>
                    {
                        // Replace the real database context with an in-memory one
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddPooledDbContextFactory<AppDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("InMemoryDbForTesting");
                        });

                        // Ensure database is created and seeded with test data
                        var sp = services.BuildServiceProvider();
                        using var scope = sp.CreateScope();
                        var scopedServices = scope.ServiceProvider;
                        var dbFactory = scopedServices.GetRequiredService<IDbContextFactory<AppDbContext>>();
                        using var db = dbFactory.CreateDbContext();
                        db.Database.EnsureCreated();
                        DataSeeder.SeedData(scopedServices);
                    });
            });

            return base.CreateHost(builder);
        }
    }
}
