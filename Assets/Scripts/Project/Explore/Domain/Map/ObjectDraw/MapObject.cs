using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;

namespace DigitalWar.Project.Explore.Domain.Map.ObjectDraw
{
    public class MapObject
    {
        public int X;
        public int Y;
        public PlayerColors Color;
        public Objects Type;
        public bool IsXDirection;

        public MapObject(int x, int y, PlayerColors color, Objects type, bool isXDirection = false)
        {
            X = GameManager.Instance.PlayerCurrentState.X + x;
            Y = GameManager.Instance.PlayerCurrentState.Y + y;
            Color = color;
            Type = type;
            IsXDirection = isXDirection;
        }
        public void Update(MapObject target)
        {
            X = target.X;
            Y = target.Y;
            Color = target.Color;
            Type = target.Type;
            IsXDirection = target.IsXDirection;
        }
    }
}
