namespace AdoApi.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime ReleaseDate { get; set; }
}
