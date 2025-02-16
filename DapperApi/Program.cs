using DapperApi.Domain.Entities;
using DapperApi.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMovieRepository, MovieRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/movies", async ([FromServices] IMovieRepository movieRepository) =>
{
    var movies = await movieRepository.GetAllAsync();
    return TypedResults.Ok(movies.ToList());
});

app.MapGet("/movies/{id:guid}",
    async Task<IResult> ([FromRoute] Guid id, [FromServices] IMovieRepository movieRepository) =>
    {
        var movie = await movieRepository.GetByIdAsync(id);
        return movie is not null ? TypedResults.Ok(movie) : TypedResults.NotFound();
    });

app.MapPost("/movies",
    async Task<IResult> ([FromBody] MovieEntity movieEntity,
        [FromServices] IMovieRepository movieRepository) =>
    {
        movieEntity.Id = Guid.NewGuid();
        var insertCount = await movieRepository.AddAsync(movieEntity);
        return insertCount == 1 ? TypedResults.Created() : TypedResults.StatusCode(500);
    });

app.MapPut("/movies",
    async Task<IResult> ([FromBody] MovieEntity movie, [FromServices] IMovieRepository movieRepository) =>
    {
        var updateCount = await movieRepository.UpdateAsync(movie);
        return updateCount == 1 ? TypedResults.NoContent() : TypedResults.StatusCode(500);
    });

app.MapDelete("/movies/{id:guid}",
    async Task<IResult> ([FromRoute] Guid id, [FromServices] IMovieRepository movieRepository) =>
    {
        var deleteCount = await movieRepository.DeleteAsync(id);
        return deleteCount == 1 ? TypedResults.NoContent() : TypedResults.StatusCode(500);
    });
app.Run();

