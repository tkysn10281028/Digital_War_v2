using System.Collections.Generic;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;

namespace DigitalWar.Project.Common.Objects.Explore
{
    public class ExploreObject
    {
        public List<MapObject> MapObjectList = new();
        public List<StatusObject> StatusObjectList = new();
        public void SetPlayerCurPosOnMap()
        {
            var curPosX = GameManager.Instance.PlayerCurrentState.X;
            var curPosY = GameManager.Instance.PlayerCurrentState.Y;
            MapObjectList.Add(new MapObject(curPosX, curPosY, GameManager.Instance.PlayerCurrentState.Color, Enums.Objects.Player, false));
        }
    }
}