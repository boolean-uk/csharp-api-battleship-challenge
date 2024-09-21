namespace exercise.wwwapi.Model.Models
{
    public class Grid
    {
        public int Size { get; }
        public CellState[,] Cells { get; }

        public Grid(int size)
        {
            Size = size;
            Cells = new CellState[size, size];
        }

        public bool IsValidCoordinate(int x, int y)
        {
            return x >= 0 && x < Size && y >= 0 && y < Size;
        }

        public void PlaceShip(Ship ship, List<(int X, int Y)> coordinates)
        {
            foreach (var (X, Y) in coordinates)
            {
                if (IsValidCoordinate(X, Y))
                {
                    Cells[X, Y] = CellState.Ship;
                    ship.Coordinates.Add((X, Y));
                }
                else
                {
                    throw new Exception("Invalid coordinate for ship placement.");
                }
            }
        }
    }
}
