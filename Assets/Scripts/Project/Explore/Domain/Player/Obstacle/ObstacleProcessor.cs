using System;
using System.Linq;
using DigitalWar.Project.Common.Dialog;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Common.MapName;
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
                    ShowChoicesAndUpdateObjectsForWall(
                        (type) => SetObjectUpAndRedrawMap(type),
                        () => MoveToNextArea(new Vector3(0, -8f))
                    );
                    return origin;

                // 下に行く
                case TileTypes.Stair01:
                    ShowChoicesAndUpdateObjectsForWall(
                        (type) => SetObjectDownAndRedrawMap(type),
                        () => MoveToNextArea(new Vector3(0, 8f))
                    );
                    return origin;

                // 左に行く
                case TileTypes.Stair05:
                    ShowChoicesAndUpdateObjectsForWall(
                        (type) => SetObjectLeftAndRedrawMap(type),
                        () => MoveToNextArea(new Vector3(15f, 0))
                    );
                    return origin;

                // 右に行く
                case TileTypes.Stair06:
                    ShowChoicesAndUpdateObjectsForWall(
                        (type) => SetObjectRightAndRedrawMap(type),
                        () => MoveToNextArea(new Vector3(-15f, 0))
                    );
                    return origin;

                // アイテムボックス
                case TileTypes.ItemBox01:
                    ShowChoicesAndUpdateObjectsForItemBox();
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

            // TODO: 現在地からマップ名を取得して再描画
            fieldDrawer.RedrawField(MapNameResolver.Resolve());

        }

        private void ShowChoicesAndUpdateObjectsForWall(Action<Objects> onSelect = null, Action onPass = null)
        {
            DialogSystem.ShowWithChoicesAsync(
                "行動を選択してください(Zで閉じる) :",
                new[] { "通過", "鍵", "ウイルス" },
                index =>
                {
                    if (index == 0)
                    {
                        GameManager.Instance.LockPlayer();
                        onSelect?.Invoke(Objects.Player);
                        onPass?.Invoke();
                        return;
                    }
                    var type = index switch
                    {
                        1 => Objects.Lock,
                        2 => Objects.Virus,
                        _ => Objects.None
                    };

                    if (type == Objects.None) return;
                    onSelect.Invoke(type);
                    RemoveAndRedrawStatus(type);
                }, false
            );
        }

        private void ShowChoicesAndUpdateObjectsForItemBox()
        {
            DialogSystem.ShowWithChoicesAsync(
                "行動を選択してください(Zで閉じる) :",
                new[] { "所有権", "抗体" },
                index =>
                {
                    var type = index switch
                    {
                        0 => Objects.Ownership,
                        1 => Objects.Resist,
                        _ => Objects.None
                    };

                    SetObjectAndRedrawMap(type);

                    if (type == Objects.None || type == Objects.Ownership) return;
                    RemoveAndRedrawStatus(type);
                }
            );
        }
        private void SetObjectAndRedrawMap(Objects type)
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
            if (GameManager.Instance.PlayerCurrentState.Y < 0 && type != Objects.Resist)
            {
                return;
            }
            var newObj = new MapObject(0, -1, GameManager.Instance.PlayerCurrentState.Color, type, false);
            SetObjectAndRedrawMapCore(newObj);

            // 再描画
            var drawer = FindFirstObjectByType<MapObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawMapObject();
            }
        }

        private void SetObjectRightAndRedrawMap(Objects type)
        {
            if (GameManager.Instance.PlayerCurrentState.X > 0 && type != Objects.Resist)
            {
                return;
            }
            var newObj = new MapObject(0, 0, GameManager.Instance.PlayerCurrentState.Color, type, true);
            SetObjectAndRedrawMapCore(newObj);

            // 再描画
            var drawer = FindFirstObjectByType<MapObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawMapObject();
            }
        }

        private void SetObjectLeftAndRedrawMap(Objects type)
        {
            if (GameManager.Instance.PlayerCurrentState.X < 0 && type != Objects.Resist)
            {
                return;
            }
            var newObj = new MapObject(-1, 0, GameManager.Instance.PlayerCurrentState.Color, type, true);
            SetObjectAndRedrawMapCore(newObj);

            // 再描画
            var drawer = FindFirstObjectByType<MapObjectDrawer>();
            if (drawer != null)
            {
                drawer.DrawMapObject();
            }
        }

        private void SetObjectAndRedrawMapCore(MapObject newObject)
        {
            var statusObjectList = GameManager.Instance.ExploreObject.StatusObjectList;
            if (newObject.Type != Objects.Ownership && !statusObjectList.Any(obj => obj.Type == newObject.Type))
                return;

            var mapObjectList = GameManager.Instance.ExploreObject.MapObjectList;
            var existing = mapObjectList.FirstOrDefault(obj =>
                obj.X == newObject.X &&
                obj.Y == newObject.Y &&
                obj.Color == newObject.Color &&
                obj.IsXDirection == newObject.IsXDirection
            );
            if (existing == null || newObject.Type == Objects.Resist || newObject.Type == Objects.Ownership)
            {
                mapObjectList.Add(newObject);
            }
            else
            {
                existing.Update(newObject);
            }
            mapObjectList.ForEach((m) => Debug.Log(m));
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
            GameManager.Instance.UnlockPlayer();
        }
    }
}