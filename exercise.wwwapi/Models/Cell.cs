namespace exercise.wwwapi.Models
{
    public class Cell
    {
        public Coordinate Coordinate { get; set; }
        public bool HasShip { get; set; } 
        public bool IsHit { get; set; }   

        public Cell(int x, int y)
        {
            Coordinate = new Coordinate(x, y);
            HasShip = false;
            IsHit = false;
        }
    }
}
