using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkApi.Entities;

[Table("movies")]
public class MovieEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }
    [Required]
    [Column("name")]
    public string? Name { get; set; }
    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }
}
