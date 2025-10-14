using DigitalWar.Project.Common.Dialog;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Player.Obstacle
{
    public class ObstacleProcessor : MonoBehaviour
    {
        public Vector3 ProcessAndReturnPosition(Vector3 origin, Vector3 target, int cellValue)
        {
            switch (cellValue)
            {
                case 1:
                    return origin;
                case 2:
                    DialogSystem.ShowWithChoicesAsync(
                        "行動を選択してください:",
                        new[] { "通過", "鍵", "ウイルス", "抗体" },
                        index =>
                        {
                            switch (index)
                            {
                                case 0:
                                    Debug.Log("通過");
                                    break;
                                case 1:
                                    Debug.Log("鍵");
                                    break;
                                case 2:
                                    Debug.Log("ウイルス");
                                    break;
                                case 3:
                                    Debug.Log("抗体");
                                    break;
                                default:
                                    break;
                            }
                        }
                    );
                    return origin;
                case 3:
                    return origin;
                case 0:
                    return target;
                default:
                    return target;
            }
        }
    }
}