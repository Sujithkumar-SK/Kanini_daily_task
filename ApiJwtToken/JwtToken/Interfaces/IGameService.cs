public interface IGameService
{
  Task<IEnumerable<Game>> GetGames();
  Task<Game> GetGame(int id);
  Task<Game> CreateGame(Game game);
  Task UpdateGame(Game game);
  Task DeleteGame(int id);
}