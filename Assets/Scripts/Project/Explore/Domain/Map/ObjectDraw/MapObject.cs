using DigitalWar.Project.Common.Enums;

namespace DigitalWar.Project.Explore.Domain.Map.ObjectDraw
{
    public class MapObject
    {
        public int X;
        public int Y;
        public int Color;
        public Objects Type;
        public bool IsXDirection;

        public MapObject(int x, int y, int color, Objects type, bool isXDirection = false)
        {
            X = x;
            Y = y;
            Color = color;
            Type = type;
            IsXDirection = isXDirection;
        }
    }
}
