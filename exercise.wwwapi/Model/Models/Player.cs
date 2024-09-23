namespace exercise.wwwapi.Model.Models
{
    public class Player
    {
        public string PlayerId { get; }
        public string Name { get; }
        public Grid OceanGrid { get; }
        public Grid TargetGrid { get; }

        public List<Ship> Fleet { get; set; }

        public Player(string name, int gridSize)
        {
            PlayerId = Guid.NewGuid().ToString();
            Name = name;
            OceanGrid = new Grid(gridSize);
            TargetGrid = new Grid(gridSize);
            Fleet = new List<Ship>();
        }
    }
}
