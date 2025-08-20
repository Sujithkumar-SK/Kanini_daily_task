using System.ComponentModel.DataAnnotations;
public class Author
{
  [Key]
  public int AuthorId { get; set; }
  [Required]
  public string? AuthorName { get; set; }
  public ICollection<BookAuthorLink>? BookAuthorLinks { get; set; } = new List<BookAuthorLink>();
}