namespace DigitalWar.Project.Explore.Domain.Player
{
    public class PlayerCurrentPosition
    {
        public int X;
        public int Y;

        public void SetPlayerPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}