using DigitalWar.Project.Common.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DigitalWar.Project.Explore.Domain.Field.Drawer
{
    public class TileSetHandler : MonoBehaviour
    {
        [SerializeField] private TileBase _wallTile01;
        [SerializeField] private TileBase _wallTile02;
        [SerializeField] private TileBase _wallTile03;
        [SerializeField] private TileBase _floorTile01;

        public void SetTile(TileTypes cellValue, Vector3Int cellPos, Tilemap tilemap)
        {
            switch (cellValue)
            {
                case TileTypes.Wall01:
                    tilemap.SetTile(cellPos, _wallTile01);
                    break;
                case TileTypes.Wall02:
                    tilemap.SetTile(cellPos, _wallTile02);
                    break;
                case TileTypes.Wall03:
                    tilemap.SetTile(cellPos, _wallTile03);
                    break;
                case TileTypes.Floor01:
                    tilemap.SetTile(cellPos, _floorTile01);
                    break;
                default:
                    break;
            }
        }
    }
}