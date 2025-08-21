namespace Backend.DTOs;

public class CreateKidDto
{
  public string? Name { get; set; }
  public int Age { get; set; }
  public string? Grade { get; set; }
  public byte[]? ProfileImage { get; set; }
  public int UserId { get; set; }
}

public class UpdateKidDto
{
  public string? Name { get; set; }
  public int Age { get; set; }
  public string? Grade { get; set; }
  public byte[]? ProfileImage { get; set; }
}
