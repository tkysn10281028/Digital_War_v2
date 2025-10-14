using UnityEngine;
namespace DigitalWar.Project.Explore.Domain.Player.IF
{
    public interface IPlayerObstacleHandler
    {
        public Vector3 JudgeIsObstacle(Vector3 origin, Vector3 target);
    }
}
