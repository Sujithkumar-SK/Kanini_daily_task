public interface IGameRepository
{
  Task<IEnumerable<Game>> GetAll();
  Task<Game> GetById(int id);
  Task Add(Game game);
  Task Update(Game game);
  Task Delete(int id);
}