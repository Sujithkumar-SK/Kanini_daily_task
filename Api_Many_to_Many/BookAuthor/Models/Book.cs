using System.ComponentModel.DataAnnotations;
public class Book
{
  [Key]
  public int BookId { get; set; }

  [Required]
  public string? BookName { get; set; }
  public int PublicationYear { get; set; }
  public decimal Price { get; set; }
  public ICollection<BookAuthorLink>?BookAuthorLinks{ get; set; }=new List<BookAuthorLink>();
}