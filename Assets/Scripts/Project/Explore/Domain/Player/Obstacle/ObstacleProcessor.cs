using System;
using System.Linq;
using DigitalWar.Project.Common.Dialog;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;
using UniRx;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Player.Obstacle
{
    public class ObstacleProcessor : MonoBehaviour
    {
        public Vector3 ProcessAndReturnPosition(Vector3 origin, Vector3 target, TileTypes cellValue)
        {
            switch (cellValue)
            {
                case TileTypes.Wall01:
                    return origin;
                case TileTypes.Wall02:
                    ShowChoicesAndUpdateObjects(
                        () => ShiftPlayerPositionIfPass(new Vector3(0, -9f)),
                        (type) => SetObjectUpAndRedrawMap(type)
                    );
                    return origin;
                case TileTypes.Wall03:
                    return origin;
                case TileTypes.Floor01:
                    return target;
                default:
                    return target;
            }
        }
        private void ShiftPlayerPositionIfPass(Vector3 move)
        {
            var playerObj = FindFirstObjectByType<PlayerMove>();
            if (playerObj == null) return;
            GameManager.Instance.LockPlayer();
            FadeController.Instance.FadeOut(0.5f)
                .Delay(TimeSpan.FromSeconds(0.2))
                .Do(_ =>
                {
                    playerObj.transform.position += move;
                })
                .SelectMany((_) => FadeController.Instance.FadeIn(0.5f))
                .Subscribe(
                    _ =>
                    {
                        GameManager.Instance.UnlockPlayer();
                        Debug.Log("通過完了");
                    },
                    ex =>
                    {
                        GameManager.Instance.UnlockPlayer();
                        Debug.LogError(ex);
                    }
                )
                .AddTo(this);
        }

        private void ShowChoicesAndUpdateObjects(Action onPass, Action<Objects> onSelect)
        {
            DialogSystem.ShowWithChoicesAsync(
                "行動を選択してください:",
                new[] { "通過", "鍵", "ウイルス", "抗体" },
                index =>
                {
                    if (index == 0)
                    {
                        Debug.Log("通過");
                        onPass.Invoke();
                        return;
                    }
                    var type = index switch
                    {
                        1 => Objects.Lock,
                        2 => Objects.Virus,
                        3 => Objects.Resist,
                        _ => Objects.None
                    };

                    if (type == Objects.None) return;
                    onSelect.Invoke(type);
                    RemoveAndRedrawStatus(type);
                }
            );
        }

        private void SetObjectUpAndRedrawMap(Objects type)
        {
            var newObj = new MapObject(0, 0, GameManager.Instance.PlayerCurrentState.Color, type, false);
            SetObjectAndRedrawMapCore(newObj);

            // 再描画
            var drawer = FindFirstObjectByType<MapObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawMapObject();
            }
        }

        private void SetObjectDownAndRedrawMap(Objects type)
        {
            var x = GameManager.Instance.PlayerCurrentState.X;
            var y = GameManager.Instance.PlayerCurrentState.Y;
            var playerColor = GameManager.Instance.PlayerCurrentState.Color;
            var newObj = new MapObject(x, y - 1, playerColor, type, false);
            SetObjectAndRedrawMapCore(newObj);
        }

        private void SetObjectRightAndRedrawMap(Objects type)
        {
            var x = GameManager.Instance.PlayerCurrentState.X;
            var y = GameManager.Instance.PlayerCurrentState.Y;
            var playerColor = GameManager.Instance.PlayerCurrentState.Color;
            var newObj = new MapObject(x, y, playerColor, type, true);
            SetObjectAndRedrawMapCore(newObj);
        }

        private void SetObjectLeftAndRedrawMap(Objects type)
        {
            var x = GameManager.Instance.PlayerCurrentState.X;
            var y = GameManager.Instance.PlayerCurrentState.Y;
            var playerColor = GameManager.Instance.PlayerCurrentState.Color;
            var newObj = new MapObject(x - 1, y, playerColor, type, true);
            SetObjectAndRedrawMapCore(newObj);
        }

        private void SetObjectAndRedrawMapCore(MapObject newObject)
        {
            var statusObjectList = GameManager.Instance.ExploreObject.StatusObjectList;
            if (!statusObjectList.Any(obj => obj.Type == newObject.Type))
                return;

            var mapObjectList = GameManager.Instance.ExploreObject.MapObjectList;
            var existing = mapObjectList.FirstOrDefault(obj =>
                obj.X == newObject.X &&
                obj.Y == newObject.Y &&
                obj.Color == newObject.Color &&
                obj.IsXDirection == newObject.IsXDirection
            );
            if (existing == null || newObject.Type == Objects.Resist)
            {
                mapObjectList.Add(newObject);
            }
            else
            {
                existing.Update(newObject);
            }
            var drawer = FindFirstObjectByType<MapObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawMapObject();
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