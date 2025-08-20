public class GameService : IGameService
{
  private readonly IGameRepository _repo;
  public GameService(IGameRepository repo)
  {
    _repo = repo;
  }
  public async Task<IEnumerable<Game>> GetGames()
  {
    return await _repo.GetAll();
  }
  public async Task<Game> GetGame(int id)
  {
    return await _repo.GetById(id);
  }
  public async Task<Game> CreateGame(Game game)
  {
    await _repo.Add(game);
    return game;
  }
  public async Task UpdateGame(Game game)
  {
    await _repo.Update(game);
  }
  public async Task DeleteGame(int id)
  {
    await _repo.Delete(id);
  }
}