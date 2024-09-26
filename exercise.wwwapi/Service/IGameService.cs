using exercise.wwwapi.Models;

namespace exercise.wwwapi.Service
{
    public interface IGameService
    {
        Game getGame();
        void InitializeGame();    
        
        bool canInitializeNewGame();
        void InitializeFleet(Player player);
        Player getPlayer(int id);
        bool IsValidCoordinate(Coordinate coordinate);   
        bool CheckIfSunk(Grid grid);                     
        string MakeMove(Player player, Coordinate targetCoordinate);
        void PlaceShip(Player player, Ship ship, Coordinate startCoordinate, bool isVertical);
        bool CanPlaceShip(Player player, Ship ship, Coordinate startCoordinate, bool isVertical);
        string CheckGameStatus();                          
        void ResetGame();


    }
}
