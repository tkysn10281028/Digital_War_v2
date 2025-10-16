using DigitalWar.Project.Common.Manager;
using UnityEngine;

namespace DigitalWar.Project.Common.MapName
{
    public class MapNameResolver
    {
        public static string Resolve()
        {
            return (GameManager.Instance.PlayerCurrentState.X, GameManager.Instance.PlayerCurrentState.Y) switch
            {
                (0, 0) => "map_noborder.csv",
                (0, 1) => "map_up_border.csv",
                (0, -1) => "map_down_border.csv",
                (1, 0) => "map_right_border.csv",
                (1, 1) => "map_rightup_border.csv",
                (1, -1) => "map_rightdown_border.csv",
                (-1, 0) => "map_left_border.csv",
                (-1, 1) => "map_leftup_border.csv",
                (-1, -1) => "map_leftdown_border.csv",
                _ => "",
            };
        }
    }
}