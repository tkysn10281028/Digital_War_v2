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
        [SerializeField] private TileBase _doorTile01;
        [SerializeField] private TileBase _stairTileDown;
        [SerializeField] private TileBase _stairTileLeft01;
        [SerializeField] private TileBase _stairTileRight01;
        [SerializeField] private TileBase _itemBox01;

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
                case TileTypes.Door01:
                    tilemap.SetTile(cellPos, _doorTile01);
                    break;
                case TileTypes.Stair01:
                    tilemap.SetTile(cellPos, _stairTileDown);
                    break;
                case TileTypes.Stair05:
                    tilemap.SetTile(cellPos, _stairTileLeft01);
                    break;
                case TileTypes.Stair06:
                    tilemap.SetTile(cellPos, _stairTileRight01);
                    break;
                case TileTypes.ItemBox01:
                    tilemap.SetTile(cellPos, _itemBox01);
                    break;
                default:
                    break;
            }
        }
    }
}