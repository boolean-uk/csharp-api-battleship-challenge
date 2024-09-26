namespace exercise.wwwapi.Models
{
    public class Move
    {

        public Coordinate Coordinate { get; set; }
        public bool IsHit { get; set; } 

        public Move(Coordinate coordinate)
        {
            Coordinate = coordinate;
            IsHit = false;
        }
    }
}
