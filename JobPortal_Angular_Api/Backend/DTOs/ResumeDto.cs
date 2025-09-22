namespace Backend.DTOs;

public class ResumeUploadDto
{
  public string FileName { get; set; } = string.Empty;
  public byte[] FileData { get; set; } = Array.Empty<byte>();
}

public class ResumeResponseDto
{
  public int ResumeId { get; set; }
  public string FileName { get; set; } = string.Empty;
  public DateTime UploadedOn { get; set; }
}