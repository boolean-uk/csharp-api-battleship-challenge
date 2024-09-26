namespace exercise.wwwapi.Models
{
    public class Grid
    {

        public const int Size = 10;
        public Cell[,] Cells {  get; set; }

        public Player player { get; set; }  


        public Grid() {

            Cells = new Cell[Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Cells[x, y] = new Cell(x, y);
                }
            }

        }

        public bool isValidCoordinate(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X < Size &&
              coordinate.Y >= 0 && coordinate.Y < Size;
        }
    }
}
