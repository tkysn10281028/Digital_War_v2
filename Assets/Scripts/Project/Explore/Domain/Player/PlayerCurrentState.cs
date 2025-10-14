using DigitalWar.Project.Common.Enums;

namespace DigitalWar.Project.Explore.Domain.Player
{
    public class PlayerCurrentState
    {
        public int X;
        public int Y;
        public PlayerColors Color = PlayerColors.Yellow;
        public PlayerCurrentState()
        {

        }

        public PlayerCurrentState(int x, int y, PlayerColors color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void SetPlayerPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}