using System.Collections.Generic;
using System.Linq;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using DigitalWar.Project.Explore.Domain.Status.ObjectDraw;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DigitalWar.Project.Explore.Domain.Status
{
    public class StatusDrawer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _statusObjectDrawerComponent;
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _statusTileYellow;
        [SerializeField] private TileBase _statusTileGreen;

        [SerializeField] private int _gridWidth = 3;
        [SerializeField] private int _gridHeight = 3;
        private StatusObjectDrawer statusObjectDrawer;

        void Awake()
        {
            statusObjectDrawer = _statusObjectDrawerComponent as StatusObjectDrawer;
        }

        void Start()
        {
            DrawStatus();
            var data = new List<StatusObject>
            {
                new (Objects.Lock),
                new (Objects.Lock),
                new (Objects.Lock),
                new (Objects.Virus),
                new (Objects.Resist),
            };
            GameManager.Instance.ExploreObject.StatusObjectList = data;
            statusObjectDrawer.DrawStatusObject();
        }

        private void DrawStatus()
        {
            var xAdjustment = 6;
            var yAdjustment = 2;
            for (int y = 0; y < _gridHeight; y++)
            {
                for (int x = 0; x < _gridWidth; x++)
                {
                    var pos = new Vector3Int(x + xAdjustment, y + yAdjustment, 0);
                    _tilemap.SetTile(pos, _statusTileYellow);
                }
            }
        }
    }
}
