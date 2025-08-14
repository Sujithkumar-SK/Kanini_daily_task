public interface IMark
{
  Task<IEnumerable<Mark>> GetAllMarks();
  Task<Mark> GetMarkById(int id);
  Task<Mark> AddMark(Mark mark);
  Task<Mark> UpdateMark(int id, Mark mark);
}