using System.Drawing;
using System.Xml.Linq;

namespace exercise.wwwapi.Models
{
    public class Ship
    {


        public string Name { get; set; }

        public int Size { get; set; }

        public List<Coordinate> Coordinates { get; set; }

        public bool isSunk { get; set; }


        public Ship(string name, int size)
        {
            Name = name;
            Size = size;
            Coordinates = new List<Coordinate>();
            isSunk = false;
        }


        public void CheckIfSunk(Grid grid)
        {
            isSunk = Coordinates.All(coord => grid.Cells[coord.X, coord.Y].IsHit);
        }

    }
}
