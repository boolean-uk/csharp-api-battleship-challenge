namespace exercise.wwwapi.Models
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public bool IsPlayer1Turn { get; set; }
        public bool IsGameOver { get; set; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            IsPlayer1Turn = true;
            IsGameOver = false;
        }
    }
}
