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
        public void SetTile(int cellValue, Vector3Int cellPos, Tilemap tilemap)
        {
            switch (cellValue)
            {
                case 1:
                    tilemap.SetTile(cellPos, _wallTile01);
                    break;
                case 2:
                    tilemap.SetTile(cellPos, _wallTile02);
                    break;
                case 3:
                    tilemap.SetTile(cellPos, _wallTile03);
                    break;
                case 0:
                    tilemap.SetTile(cellPos, _floorTile01);
                    break;
            }
        }
    }
}