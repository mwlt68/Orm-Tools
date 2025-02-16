using AdoApi.Entities;
using Npgsql;

namespace AdoApi.Repositories;

public class MovieRepository(IConfiguration configuration) : IMovieRepository
{
    private readonly string? _connectionString = configuration.GetConnectionString("MovieAppDb");

    public async Task<int> AddAsync(MovieEntity movieEntity)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using var command = new NpgsqlCommand("INSERT INTO movies (id, name,release_date) VALUES (@Id, @Name,@ReleaseDate)", connection);
        command.Parameters.AddWithValue("@Id", movieEntity.Id);
        command.Parameters.AddWithValue("@Name", movieEntity.Name);
        command.Parameters.AddWithValue("@ReleaseDate", movieEntity.ReleaseDate);
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<MovieEntity?> GetByIdAsync(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using var command = new NpgsqlCommand("SELECT * FROM movies WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (reader.Read())
        {
            return new MovieEntity
            {
                Id = (Guid)reader["id"],
                Name = (string)reader["name"],
                ReleaseDate =(DateTime)reader["release_date"]
            };
        }

        return null;
    }

    public async Task<IEnumerable<MovieEntity>> GetAllAsync()
    {
        var movies = new List<MovieEntity>();

        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using var command = new NpgsqlCommand("SELECT * FROM movies", connection);
        await using var reader = await command.ExecuteReaderAsync();
        while (reader.Read())
        {
            movies.Add(new MovieEntity()
            {
                Id = (Guid)reader["id"],
                Name = (string)reader["name"],
                ReleaseDate =(DateTime)reader["release_date"]
            });
        }

        return movies;
    }

    public async Task<int> UpdateAsync(MovieEntity movieEntity)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using var command = new NpgsqlCommand("UPDATE movies SET name = @Name, release_date = @ReleaseDate WHERE id = @Id", connection);
        command.Parameters.AddWithValue("@Id", movieEntity.Id);
        command.Parameters.AddWithValue("@Name", movieEntity.Name);
        command.Parameters.AddWithValue("@ReleaseDate", movieEntity.ReleaseDate);
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        await using var command = new NpgsqlCommand("DELETE FROM movies WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        return await command.ExecuteNonQueryAsync();
    }
}
