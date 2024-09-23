using exercise.wwwapi.Model.Models;
using exercise.wwwapi.Model.Data;


namespace exercise.wwwapi.Controller.Repository
{
    public class BattleShipRepository : IBattleShipRepository
    {
        public Game CreateGame(string player1Name, string player2Name, int gridSize)
        {
            var game = new Game(player1Name, player2Name, gridSize);

            GameCollection.AddGame(game);

            return game;
        }

        public Game GetGameById(string gameId)
        {
            return GameCollection.GetGameById(gameId);
        }

        public void UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }

        public void ResetGame(string gameId)
        {
            GameCollection.RemoveGameById(gameId);
        }
    }
}
