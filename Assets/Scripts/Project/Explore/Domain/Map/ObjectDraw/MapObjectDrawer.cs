using System;
using System.Collections.Generic;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DigitalWar.Project.Explore.Domain.Map.ObjectDraw
{
    public class MapObjectDrawer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _mapTileYellow;
        [SerializeField] private TileBase _mapTileGreen;
        [SerializeField] private GameObject _playerYellow;
        [SerializeField] private GameObject _playerGreen;
        [SerializeField] private GameObject _regist;
        [SerializeField] private GameObject _lockYellow;
        [SerializeField] private GameObject _lockGreen;
        [SerializeField] private GameObject _virus;
        [SerializeField] private Transform _mapParent;
        private Dictionary<Objects, Action<MapObject>> mapObjectActionMap;

        void Awake()
        {
            mapObjectActionMap = new()
            {
                {
                    Objects.Player, obj =>
                    {
                        Vector3 basePos = _tilemap.CellToWorld(new Vector3Int(obj.X + 8, obj.Y -3));
                        if(obj.Color == 1)
                            Instantiate(_playerYellow, basePos - new Vector3(0.5f, 0.5f), Quaternion.identity, _mapParent);
                        else
                            Instantiate(_playerGreen, basePos - new Vector3(0.5f, 0.5f), Quaternion.identity, _mapParent);
                    }
                },
                {
                    Objects.Resist, obj =>
                    {
                        GameObject instance = null;
                        Vector3 basePos = _tilemap.CellToWorld(new Vector3Int(obj.X + 8, obj.Y -3));
                        instance = Instantiate(_regist, basePos - new Vector3(0.5f, 0.5f), Quaternion.identity, _mapParent);
                        instance.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                },
                {
                    Objects.Lock, obj =>
                    {
                        GameObject instance = null;
                        Vector3 basePos = _tilemap.CellToWorld(new Vector3Int(obj.X + 8, obj.Y -3));
                        if(obj.IsXDirection)
                            instance = Instantiate(_lockYellow, basePos - new Vector3(0, 0.5f), Quaternion.identity, _mapParent);
                        else
                            instance = Instantiate(_lockGreen, basePos - new Vector3(0.5f, 0), Quaternion.identity, _mapParent);
                        instance.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                },
                {
                    Objects.Ownership, obj =>
                    {
                        var xAdjustment = 7;
                        var yAdjustment = -4;
                        var basePos = new Vector3Int(obj.X + xAdjustment, obj.Y + yAdjustment, 0);
                        if(obj.Color == 1)
                            _tilemap.SetTile(basePos, _mapTileYellow);
                        else
                            _tilemap.SetTile(basePos, _mapTileGreen);
                    }
                },
                {
                    Objects.Virus, obj =>
                    {
                        GameObject instance = null;
                        Vector3 basePos = _tilemap.CellToWorld(new Vector3Int(obj.X + 8, obj.Y -3));
                        if(obj.IsXDirection)
                            instance = Instantiate(_virus, basePos - new Vector3(0, 0.5f), Quaternion.identity, _mapParent);
                        else
                            instance = Instantiate(_virus, basePos - new Vector3(0.5f, 0), Quaternion.identity, _mapParent);
                        instance.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }
                },
            };
        }

        public void DrawMapObject()
        {
            var data = GameManager.Instance.ExploreObject.MapObjectList;
            foreach (var item in data)
            {
                InstantiateMapObject(item);
            }
        }

        private void InstantiateMapObject(MapObject target)
        {

            if (mapObjectActionMap.TryGetValue(target.Type, out var action))
            {
                action.Invoke(target);
            }
        }
    }
}