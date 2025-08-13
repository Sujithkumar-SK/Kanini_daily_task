public interface IMark
{
  Task<IEnumerable<Mark>> GetAllMarks();
  Task<Mark> GetMarkById(int id);
}