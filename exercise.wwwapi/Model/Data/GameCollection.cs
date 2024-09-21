using exercise.wwwapi.Model.Models;
using System.Collections.Concurrent;

namespace exercise.wwwapi.Model.Data
{
    public static class GameCollection
    {

        private static List<Game> games = new List<Game>();

        public static List<Game> GetGames()
        {
            return games;
        }

        public static Game GetGameById(string gameId)
        {
            return games.FirstOrDefault(game => game.GameId == gameId);
        }

        public static Game AddGame(Game game)
        {
            games.Add(game);
            return game;
        }

        public static Game RemoveGame(Game game)
        {
            games.Remove(game);
            return game;
        }

        public static Game RemoveGameById(string gameId)
        {
            var game = GetGameById(gameId);
            if (game != null)
            {
                games.Remove(game);
            }
            return game;
        }
    }
}
