namespace exercise.wwwapi.Model.Models
{
    public class Ship
    {
        public string Type { get; set; }
        public int Size { get; set; }
        public bool IsSunk { get; set; }
        public List<(int X, int Y)> Coordinates { get; set; }

        public Ship(string type, int size)
        {
            Type = type;
            Size = size;
            IsSunk = false;
            Coordinates = new List<(int X, int Y)>();
        }
    }
}
