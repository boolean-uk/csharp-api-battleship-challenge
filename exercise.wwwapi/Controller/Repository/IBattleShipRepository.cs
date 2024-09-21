using exercise.wwwapi.Model.Models;

namespace exercise.wwwapi.Controller.Repository
{
    public interface IBattleShipRepository
    {
        Game CreateGame(string player1Name, string player2Name, int gridSize);
        Game GetGameById(string gameId);
        void UpdateGame(Game game);
        void ResetGame(string gameId);
    }
}
