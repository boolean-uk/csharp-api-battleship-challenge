using exercise.wwwapi.Models;
using System.Diagnostics.Eventing.Reader;

namespace exercise.wwwapi.Service
{
    public class GameService : IGameService
    {
        private readonly Game _game;

        public GameService(Player player1, Player player2)
        {
            _game = new Game(player1, player2);
        }

        public Game getGame()
        {
            return _game;
        }
        public void InitializeFleet(Player player)
        {
            player.Fleet = new List<Ship>
        {
            new Ship("Carrier", 5),
            new Ship("Battleship", 4),
            new Ship("Cruiser", 3),
            new Ship("Submarine", 2),
            new Ship("Destroyer", 1)
        };

        }
        public void InitializeGame()
        {

            InitializeFleet(_game.Player1);
            InitializeFleet(_game.Player2);
        }

        public bool isPlayer1(int id)
        {
            if (id == 1) return true;
            else
            {
                return false;
            }
        }

      

        public Player getPlayer(int id) {
            if (id == 1)
            {
                return _game.Player1;
            }
            else if (id == 2)
            {
                return _game.Player2;
            }
            else
            {
                return null;
            }
        }

        public bool IsValidCoordinate(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X < Grid.Size &&
                   coordinate.Y >= 0 && coordinate.Y < Grid.Size;
        }

        public bool CanPlaceShip(Player player, Ship ship, Coordinate startCoordinate, bool isVertical)
        {

            for (int i = 0; i < ship.Size; i++)
            {
                int x = isVertical ? startCoordinate.X + i : startCoordinate.X;
                int y = isVertical ? startCoordinate.Y : startCoordinate.Y + i;
                if (x >= Grid.Size || y >= Grid.Size || player.OceanGrid.Cells[x, y].HasShip)
                {
                    return false;
                }
            }
            return true;

        }

        public string CheckGameStatus()
        {
            if (_game.Player1.Fleet.All(ship => ship.isSunk))
            {
                _game.IsGameOver = true;
                _game.Player2.isWinner = true;
                return "Game over, Winner is = Player 2 !";
            }

            else if (_game.Player2.Fleet.All(ship => ship.isSunk))
            {
                _game.IsGameOver = true;
                _game.Player1.isWinner = true;
                return "Game over, Winner is = Player 1 !";

            }
            return "Game not over yet !";
        }

        public bool CheckIfSunk(Grid grid)
        {

            foreach (var ship in grid.player.Fleet)
            {
                if (ship.Coordinates.All(coord => grid.Cells[coord.X, coord.Y].IsHit))
                {
                    ship.isSunk = true;
                    return true;
                }

                
            }
            return false;
        }

        private Player GetOpponent(Player player)
        {
            return player == _game.Player1 ? _game.Player2 : _game.Player1;
        }

        public string MakeMove(Player player, Coordinate targetCoordinate)
        {
            string response = "";
            Player opponent = GetOpponent(player);

            var targetCellOnOpponentOceanGrid = opponent.OceanGrid.Cells[targetCoordinate.X, targetCoordinate.Y];

            var targetCellOnPlayerTargetGrid = player.TargetGrid.Cells[targetCoordinate.X, targetCoordinate.Y];

            if (targetCellOnPlayerTargetGrid.IsHit)
            {
                throw new InvalidOperationException(response+"This coordinate has already been targeted.");
            }

            targetCellOnPlayerTargetGrid.IsHit = true;

            if (targetCellOnOpponentOceanGrid.HasShip)
            {
                targetCellOnPlayerTargetGrid.HasShip = true;  
                targetCellOnOpponentOceanGrid.IsHit = true;  

                var hitShip = opponent.Fleet.FirstOrDefault(ship =>
                ship.Coordinates.Any(coord => coord.X == targetCoordinate.X && coord.Y == targetCoordinate.Y));
                response += "Hit !";
                if (hitShip != null)
                {
                    if (CheckIfSunk(opponent.OceanGrid))
                    {
                        response += hitShip.Name + "Has been sunk !";
                    };  
                }
            }
            else
            {
                targetCellOnPlayerTargetGrid.HasShip = false;
                return "Miss !";
            }

            _game.IsPlayer1Turn = !_game.IsPlayer1Turn;
            return response;
        }

        public void PlaceShip(Player player, Ship ship, Coordinate startCoordinate, bool isVertical)
        {
            if (!CanPlaceShip(player, ship, startCoordinate, isVertical))
            {
                throw new InvalidOperationException("Cannot place ship in this location");
            }

            if (isVertical) {
                for (int i = 0; i < ship.Size; i++)
                {
                    player.OceanGrid.Cells[startCoordinate.X + i, startCoordinate.Y].HasShip = true;
                    ship.Coordinates.Add(new Coordinate(startCoordinate.X + i, startCoordinate.Y));
                }
            }
            else
            {
                for (int i = 0; i < ship.Size; i++)
                {
                    player.OceanGrid.Cells[startCoordinate.X, startCoordinate.Y + i].HasShip = true;
                    ship.Coordinates.Add(new Coordinate(startCoordinate.X, startCoordinate.Y + i));
                }
            }
            if (player.name == _game.Player1.name){
                _game.IsPlayer1Turn = !_game.IsPlayer1Turn;
                _game.Player1.Fleet.Remove(ship);
            }
            else if (player.name == _game.Player2.name)
            {
                _game.IsPlayer1Turn = true;
                _game.Player2.Fleet.Add(ship);
            }
                
        }


        public void ResetGame()
        {
            _game.Player1.OceanGrid = new Grid();
            _game.Player1.TargetGrid = new Grid();
            _game.Player2.OceanGrid= new Grid();
            _game.Player2.TargetGrid = new Grid();

            InitializeFleet(_game.Player1);
            InitializeFleet(_game.Player2);

            _game.IsGameOver = true;
            _game.IsPlayer1Turn = true;


            _game.Player1.isWinner = false;
            _game.Player2.isWinner = false;
        }

        public bool canInitializeNewGame()
        {
            if (_game.Player1 == null || _game.Player2 == null)
            {
                return false; 
            }

            return !_game.Player1.Fleet.Any() && !_game.Player2.Fleet.Any(); 
        }
    }
}

