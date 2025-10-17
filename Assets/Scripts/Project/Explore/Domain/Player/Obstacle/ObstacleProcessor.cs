using DigitalWar.Project.Common.Enums;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Player.Obstacle
{
    public class ObstacleProcessor : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _obstacleCollideProcessor;
        private ObstacleCollideProcessor obstacleCollideProcessor;
        void Awake()
        {
            obstacleCollideProcessor = _obstacleCollideProcessor as ObstacleCollideProcessor;
        }

        public Vector3 ProcessAndReturnPosition(Vector3 origin, Vector3 target, TileTypes cellValue)
        {
            switch (cellValue)
            {
                // 以下処理不要パターン(障害物)
                case TileTypes.Wall01:
                case TileTypes.Wall02:
                case TileTypes.Wall03:
                    return origin;

                // 以下処理不要パターン(Not障害物)
                case TileTypes.Floor01:
                default:
                    return target;

                // 以下処理が必要なパターン
                // 上に行く
                case TileTypes.Door01:
                    obstacleCollideProcessor.ShowChoicesUp();
                    return origin;

                // 下に行く
                case TileTypes.Stair01:
                    obstacleCollideProcessor.ShowChoicesDown();
                    return origin;

                // 左に行く
                case TileTypes.Stair05:
                    obstacleCollideProcessor.ShowChoicesLeft();
                    return origin;

                // 右に行く
                case TileTypes.Stair06:
                    obstacleCollideProcessor.ShowChoicesRight();
                    return origin;

                // アイテムボックス
                case TileTypes.ItemBox01:
                    obstacleCollideProcessor.ShowChoicesItemBox();
                    return origin;
            }
        }
    }
}