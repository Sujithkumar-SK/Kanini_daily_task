using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.Data.SqlClient;
public class Program
{
  static string con = "Server=SKEMPIRE;Database=MovieReview;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=True";
  public static void Main(String[] args)
  {
    Console.WriteLine("Welcome to the Movie Review Aplication");
    while (true)
    {
      Console.WriteLine("1. Add Movie");
      Console.WriteLine("2. Update Movie");
      Console.WriteLine("3. View Movies");
      Console.WriteLine("4. Delete Movies");
      Console.WriteLine("5. Movies Count");
      Console.WriteLine("6. Search Movie");
      Console.WriteLine("7. Exit");
      Console.Write("Please select an option: ");

      int choice = Convert.ToInt32(Console.ReadLine());

      switch (choice)
      {
        case 1:
          AddMovie();
          break;
        case 2:
          UpdateMovie();
          break;
        case 3:
          ViewMovies();
          break;
        case 4:
          DeleteMovies();
          break;
        case 5:
          MoviesCount();
          break;
        case 6:
          SearchMovie();
          break;
        case 7:
          Console.WriteLine("Exiting the application. Goodbye!");
          return;
        default:
          Console.WriteLine("Invalid choice, please try again.");
          break;
      }
    }
  }
  static void AddMovie()
  {
    Console.Write("Id:"); int Id = int.Parse(Console.ReadLine()!);
    Console.Write("Title: "); string title = Console.ReadLine()!;
    Console.Write("DirectorName: "); string DirectorName = Console.ReadLine()!;
    Console.Write("Genre: "); string genre = Console.ReadLine()!;
    Console.Write("Release Year: "); int year = int.Parse(Console.ReadLine()!);
    Console.Write("Rating: "); decimal rating = decimal.Parse(Console.ReadLine()!);
    Console.Write("Review: "); string review = Console.ReadLine()!;

    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_AddMovie", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Id", Id);
      cmd.Parameters.AddWithValue("@Title", title);
      cmd.Parameters.AddWithValue("@DirectorName", DirectorName);
      cmd.Parameters.AddWithValue("@Genre", genre);
      cmd.Parameters.AddWithValue("@ReleaseYear", year);
      cmd.Parameters.AddWithValue("@Rating", rating);
      cmd.Parameters.AddWithValue("@Review", review);

      conn.Open();
      cmd.ExecuteNonQuery();
      Console.WriteLine("Movie added successfully!");
    }
  }
  static void UpdateMovie()
  {
    Console.Write("Id: "); int Id = int.Parse(Console.ReadLine()!);
    Console.Write("Title: "); string title = Console.ReadLine()!;
    Console.Write("Director: "); string DirectorName = Console.ReadLine()!;
    Console.Write("Genre: "); string genre = Console.ReadLine()!;
    Console.Write("Release Year: "); int year = int.Parse(Console.ReadLine()!);
    Console.Write("Rating: "); decimal rating = decimal.Parse(Console.ReadLine()!);
    Console.Write("Review: "); string review = Console.ReadLine()!;

    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_UpdateMovie", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Id", Id);
      cmd.Parameters.AddWithValue("@Title", title);
      cmd.Parameters.AddWithValue("@DirectorName", DirectorName);
      cmd.Parameters.AddWithValue("@Genre", genre);
      cmd.Parameters.AddWithValue("@ReleaseYear", year);
      cmd.Parameters.AddWithValue("@Rating", rating);
      cmd.Parameters.AddWithValue("@Review", review);

      conn.Open();
      cmd.ExecuteNonQuery();
      Console.WriteLine("Movie updated successfully!");
    }
  }
  static void ViewMovies()
  {
    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_GetMovies", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      conn.Open();
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        while (reader.Read())
        {
          Console.WriteLine($"Id: {reader["Id"]}, Title: {reader["Title"]}, Director: {reader["DirectorName"]}, Genre: {reader["Genre"]}, Release Year: {reader["ReleaseYear"]}, Rating: {reader["Rating"]}, Review: {reader["Review"]}");
        }
      }
    }
  }
  static void DeleteMovies()
  {
    Console.Write("Id: "); int Id = int.Parse(Console.ReadLine()!);
    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_DeleteMovie", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Id", Id);
      conn.Open();
      cmd.ExecuteNonQuery();
      Console.WriteLine($"movie with Id: {Id} is deleted successfully!");
    }
  }
  static void MoviesCount()
  {
    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_GetMoviesCount", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      SqlParameter output = new SqlParameter("@Count", SqlDbType.Int)
      {
        Direction = ParameterDirection.Output
      };
      cmd.Parameters.Add(output);

      conn.Open();
      cmd.ExecuteNonQuery();

      int count = (int)cmd.Parameters["@Count"].Value;
      Console.WriteLine($"Total number of movies: {count}");
    }
  }
  static void SearchMovie()
  {
    Console.Write("Enter movie title to search: ");
    string title = Console.ReadLine()!;

    using (SqlConnection conn = new SqlConnection(con))
    using (SqlCommand cmd = new SqlCommand("sp_SearchMovie", conn))
    {
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Title", title);

      conn.Open();
      using (SqlDataReader reader = cmd.ExecuteReader())
      {
        if (reader.HasRows)
        {
          while (reader.Read())
          {
            Console.WriteLine($"Id: {reader["Id"]}, Title: {reader["Title"]}, Director: {reader["DirectorName"]}, Genre: {reader["Genre"]}, Release Year: {reader["ReleaseYear"]}, Rating: {reader["Rating"]}, Review: {reader["Review"]}");
          }
        }
        else
        {
          Console.WriteLine("No movies found with the given title.");
        }
      }
    }
  }
}

