using DigitalWar.Project.Explore.Domain.Player.IF;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace DigitalWar.Project.Explore.Domain.Player.Obstacle
{
    public class PlayerTilemapObstacleHandler : MonoBehaviour, IPlayerObstacleHandler
    {
        [SerializeField] private Tilemap tilemap;
        private int[,] mapData;
        [SerializeField] private MonoBehaviour _obstacleProcessHandlerComponent;
        private ObstacleProcessHandler processor;
        void Awake()
        {
            processor = _obstacleProcessHandlerComponent as ObstacleProcessHandler;
        }

        public void Init(int[,] mapData)
        {
            this.mapData = mapData;
        }

        public Vector3 JudgeIsObstacle(Vector3 origin, Vector3 target)
        {
            Vector3Int cellPos = tilemap.WorldToCell(target);
            int cols = mapData.GetLength(1);
            int rows = mapData.GetLength(0);
            int x = cellPos.x + cols / 2;
            int y = -(cellPos.y - (rows / 2 - 1));
            if (x < 0 || x >= cols || y < 0 || y >= rows) return origin;
            int cellValue = mapData[y, x];
            return processor.ProcessAndReturnPosition(origin, target, cellValue);
        }
    }
}