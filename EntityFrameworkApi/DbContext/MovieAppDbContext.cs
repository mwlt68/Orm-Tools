using EntityFrameworkApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi.DbContext;

public class MovieAppDbContext(IConfiguration configuration) : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<MovieEntity> Movies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("MovieAppDb"));
    }
}
