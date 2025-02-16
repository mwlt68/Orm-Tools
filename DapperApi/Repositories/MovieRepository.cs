using Dapper;
using DapperApi.Domain.Entities;
using Npgsql;

namespace DapperApi.Domain.Repositories;

public class MovieRepository(IConfiguration configuration) : IMovieRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("MovieAppDb");

    public async Task<int> AddAsync(MovieEntity movieEntity)
    {
        return await new NpgsqlConnection(_connectionString)
            .ExecuteAsync(
                sql: """
                     insert into movies (id,name,release_date)
                     values (@Id,@Name,@ReleaseDate)
                     """, param: movieEntity);
    }

    public async Task<MovieEntity?> GetByIdAsync(Guid id)
    {
        return await new NpgsqlConnection(_connectionString)
            .QuerySingleOrDefaultAsync<MovieEntity?>(
                sql: "SELECT id, name, release_date AS ReleaseDate FROM movies where id= @id", new { id });
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        return await new NpgsqlConnection(_connectionString)
            .QueryAsync<MovieEntity>(
                sql: "SELECT id, name, release_date AS ReleaseDate FROM movies");
    }

    public async Task<int> UpdateAsync(MovieEntity movieEntity)
    {
        return await new NpgsqlConnection(_connectionString)
            .ExecuteAsync(
                sql: """
                     update movies
                     set name=@Name,release_date=@ReleaseDate
                     where id=@Id
                     """, param: movieEntity);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        return await new NpgsqlConnection(_connectionString)
            .ExecuteAsync(
                sql: """
                     delete from movies
                     where id=@Id
                     """, new { id });
    }
}
