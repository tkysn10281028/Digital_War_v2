using System;
using System.Linq;
using DigitalWar.Project.Common.Dialog;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Field;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;
using UniRx;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Player.Obstacle
{
    public class ObstacleProcessor : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _fieldDrawerComponent;
        [SerializeField] private MonoBehaviour _mapObjectDrawerComponent;
        [SerializeField] private MonoBehaviour _statusObjectDrawerComponent;
        [SerializeField] private Transform _player;
        private FieldDrawer fieldDrawer;
        private MapObjectDrawer mapObjectDrawer;
        private StatusObjectDrawer statusObjectDrawer;

        void Awake()
        {
            fieldDrawer = _fieldDrawerComponent as FieldDrawer;
            mapObjectDrawer = _mapObjectDrawerComponent as MapObjectDrawer;
            statusObjectDrawer = _statusObjectDrawerComponent as StatusObjectDrawer;
        }

        public Vector3 ProcessAndReturnPosition(Vector3 origin, Vector3 target, TileTypes cellValue)
        {
            switch (cellValue)
            {
                // 以下処理不要パターン
                case TileTypes.Wall01:
                case TileTypes.Wall03:
                    return origin;
                case TileTypes.Floor01:
                    return target;
                default:
                    return target;

                // 以下処理が必要なパターン
                case TileTypes.Wall02:
                    ShowChoicesAndUpdateObjects(
                        (type) => SetObjectUpAndRedrawMap(type),
                        () => MoveToNextArea(new Vector3(0, -9f))
                    );
                    return origin;
            }
        }

        private void MoveToNextArea(Vector3 move)
        {
            GameManager.Instance.LockPlayer();
            FadeController.Instance.FadeOut(0.5f)
                .Delay(TimeSpan.FromSeconds(0.2))
                .Do(_ =>
                {
                    MoveToNextAreaCore(move);
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

        private void MoveToNextAreaCore(Vector3 move)
        {
            // もらった移動量の分だけプレイヤーの位置を移動
            _player.position += move;
            var cur = -move.normalized;

            // TODO: 現在地からマップ名を取得して再描画
            fieldDrawer.RedrawField("map.csv");

            // TODO: マップの再描画
            var mapObjectList = GameManager.Instance.ExploreObject.MapObjectList;
            var target = mapObjectList.FirstOrDefault(obj => obj.Type == Objects.Player);
            if (target != null)
            {
                mapObjectList.Remove(target);
            }
            mapObjectList.Add(new MapObject((int)cur.x, (int)cur.y, GameManager.Instance.PlayerCurrentState.Color, Objects.Player, false));
            mapObjectDrawer.DrawMapObject();
            GameManager.Instance.PlayerCurrentState.SetPlayerPosition((int)cur.x, (int)cur.y);
        }

        private void ShowChoicesAndUpdateObjects(Action<Objects> onSelect = null, Action onPass = null)
        {
            DialogSystem.ShowWithChoicesAsync(
                "行動を選択してください:",
                new[] { "通過", "鍵", "ウイルス", "抗体" },
                index =>
                {
                    if (index == 0)
                    {
                        onSelect?.Invoke(Objects.Player);
                        onPass?.Invoke();
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
            if (GameManager.Instance.PlayerCurrentState.Y > 0 && type != Objects.Resist)
            {
                return;
            }
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
            mapObjectDrawer.DrawMapObject();
        }

        private void RemoveAndRedrawStatus(Objects type)
        {
            var list = GameManager.Instance.ExploreObject.StatusObjectList;
            var target = list.FirstOrDefault(obj => obj.Type == type);
            if (target != null)
            {
                list.Remove(target);
            }
            statusObjectDrawer.DrawStatusObject();
        }
    }
}