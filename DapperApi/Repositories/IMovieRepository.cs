using DapperApi.Domain.Entities;

namespace DapperApi.Domain.Repositories;

public interface IMovieRepository
{
    public Task<int> AddAsync(MovieEntity movieEntity);
    public Task<MovieEntity?> GetByIdAsync(Guid id);
    public Task<IEnumerable<MovieEntity>> GetAllAsync();
    public Task<int> UpdateAsync(MovieEntity movieEntity);
    public Task<int> DeleteAsync(Guid id);
}
