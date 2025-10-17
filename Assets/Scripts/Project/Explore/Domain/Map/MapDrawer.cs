using System.Collections.Generic;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Map.ObjectDraw;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace DigitalWar.Project.Explore.Domain.Map
{
    public class MapDrawer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _mapObjectDrawerComponent;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _defaultMapTile;
        [SerializeField] private int _gridWidth = 3;
        [SerializeField] private int _gridHeight = 3;
        private MapObjectDrawer mapObjectDrawer;

        void Awake()
        {
            mapObjectDrawer = _mapObjectDrawerComponent as MapObjectDrawer;
        }

        void Start()
        {
            DrawMap();
            mapObjectDrawer.DrawMapObject();
        }

        private void DrawMap()
        {
            var xAdjustment = 6;
            var yAdjustment = -5;
            for (int y = 0; y < _gridHeight; y++)
            {
                for (int x = 0; x < _gridWidth; x++)
                {
                    var pos = new Vector3Int(x + xAdjustment, y + yAdjustment, 0);
                    _tilemap.SetTile(pos, _defaultMapTile);
                }
            }
        }
    }
}
