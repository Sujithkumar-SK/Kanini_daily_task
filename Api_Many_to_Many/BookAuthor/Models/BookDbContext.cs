using Microsoft.EntityFrameworkCore;
public class BookDbContext : DbContext
{
  public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
  public DbSet<Book> Books { get; set; }
  public DbSet<Author> Authors { get; set; }
  public DbSet<BookAuthorLink> BookAuthorLinks { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<BookAuthorLink>()
      .HasKey(o => new { o.BookId, o.AuthorId });
    modelBuilder.Entity<BookAuthorLink>()
      .HasOne(o => o.Book)
      .WithMany(o => o.BookAuthorLinks)
      .HasForeignKey(o => o.BookId);
    modelBuilder.Entity<BookAuthorLink>()
      .HasOne(o => o.Author)
      .WithMany(o => o.BookAuthorLinks)
      .HasForeignKey(o => o.AuthorId);
    modelBuilder.Entity<Book>()
      .HasData(
        new Book { BookId = 1, BookName = "Book One", PublicationYear = 2020, Price = 19.99M },
        new Book { BookId = 2, BookName = "Book Two", PublicationYear = 2021, Price = 29.99M }
      );
    modelBuilder.Entity<Author>()
      .HasData(
        new Author { AuthorId = 1, AuthorName = "Author One" },
        new Author { AuthorId = 2, AuthorName = "Author Two" }
      );
    modelBuilder.Entity<BookAuthorLink>()
      .HasData(
        new BookAuthorLink { BookId = 1, AuthorId = 1 },
        new BookAuthorLink { BookId = 1, AuthorId = 2 }
      );
  }
}