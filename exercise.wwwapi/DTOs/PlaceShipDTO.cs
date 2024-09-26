using exercise.wwwapi.Models;

namespace exercise.wwwapi.DTOs
{
    public class PlaceShipDTO
    {

        public int playerId { get; set; }

        public string shipName { get; set; }

        public int xAxis { get; set; }

        public int yAxis { get; set; }
        public bool isVertical { get; set; }
    }
}
