using EntityFrameworkApi.DbContext;
using EntityFrameworkApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkApi.Repositories;

public class MovieRepository(MovieAppDbContext context) : IMovieRepository
{

    public async Task<int> AddAsync(MovieEntity movieEntity)
    {
        context.Movies.Add(movieEntity);
        return await context.SaveChangesAsync();
    }

    public async Task<MovieEntity?> GetByIdAsync(Guid id) => await context.Movies.SingleOrDefaultAsync(x => x.Id == id);

    public async Task<IEnumerable<MovieEntity>> GetAllAsync() => await context.Movies.ToListAsync();

    public async Task<int> UpdateAsync(MovieEntity movieEntity)
    {
        context.Movies.Update(movieEntity);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Guid id) => await context.Movies.Where(x=> x.Id==id).ExecuteDeleteAsync();
}
