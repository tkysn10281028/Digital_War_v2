using System.Linq;
using DigitalWar.Project.Common.Dialog;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Status;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;
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
                                    RemoveAndRedrawStatus(Objects.Lock);
                                    break;
                                case 2:
                                    RemoveAndRedrawStatus(Objects.Virus);
                                    break;
                                case 3:
                                    RemoveAndRedrawStatus(Objects.Resist);
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
        private void RemoveAndRedrawStatus(Objects type)
        {
            var list = GameManager.Instance.ExploreObject.StatusObjectList;
            var target = list.FirstOrDefault(obj => obj.Type == type);
            if (target != null)
            {
                list.Remove(target);
            }
            var drawer = FindFirstObjectByType<StatusObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawStatusObject();
            }
        }
    }
}