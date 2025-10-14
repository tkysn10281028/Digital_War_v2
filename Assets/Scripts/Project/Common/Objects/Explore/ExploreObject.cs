using System.Collections.Generic;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;

namespace DigitalWar.Project.Common.Objects.Explore
{
    public class ExploreObject
    {
        public List<MapObject> MapObjectList = new();
        public List<StatusObject> StatusObjectList = new();
    }
}