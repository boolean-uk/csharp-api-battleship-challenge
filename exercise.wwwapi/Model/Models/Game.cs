using System.Numerics;

namespace exercise.wwwapi.Model.Models
{
    public class Game
    {
        public string GameId { get; }
        public Player Player1 { get; }
        public Player Player2 { get; }
        public Player CurrentTurn { get; set; }

        public Game(string player1Name, string player2Name, int gridSize)
        {
            GameId = Guid.NewGuid().ToString();
            Player1 = new Player(player1Name, gridSize);
            Player2 = new Player(player2Name, gridSize);
            CurrentTurn = Player1;
        }

        public bool TakeTurn(Player player, int x, int y)
        {
            var opponent = player == Player1 ? Player2 : Player1;

            if (!opponent.OceanGrid.IsValidCoordinate(x, y))
                throw new Exception("Invalid move coordinates.");

            var cellState = opponent.OceanGrid.Cells[x, y];

            if (cellState == CellState.Empty)
            {
                opponent.OceanGrid.Cells[x, y] = CellState.Miss;
                player.TargetGrid.Cells[x, y] = CellState.Miss;
            }
            else if (cellState == CellState.Ship)
            {
                opponent.OceanGrid.Cells[x, y] = CellState.Hit;
                player.TargetGrid.Cells[x, y] = CellState.Hit;

                foreach (var ship in opponent.Fleet)
                {
                    if (ship.Coordinates.Any(coord => coord.X == x && coord.Y == y))
                    {
                        ship.Coordinates.RemoveAll(coord => coord.X == x && coord.Y == y);
                        if (ship.Coordinates.Count == 0)
                        {
                            ship.IsSunk = true;
                        }
                        break;
                    }
                }
            }
            else
            {
                throw new Exception("Cell has already been targeted.");
            }

            bool gameWon = opponent.Fleet.All(s => s.IsSunk);

            CurrentTurn = opponent;

            return gameWon;
        }

        public Player GetPlayerById(string playerId)
        {
            if (Player1.PlayerId == playerId)
                return Player1;
            else if (Player2.PlayerId == playerId)
                return Player2;
            else
                return null;
        }

    }
}
