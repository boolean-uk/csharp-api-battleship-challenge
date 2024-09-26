namespace exercise.wwwapi.Models
{
    public class Player
    {

        public string name {  get; set; }

        public Grid OceanGrid { get; set; }

        public Grid TargetGrid { get; set; }

        public List<Ship> Fleet { get; set; }

        public bool isWinner { get; set; }  
    }
}
