// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class StatusDrawer : MonoBehaviour
// {
//     [SerializeField] private Tilemap tilemap;
//     [SerializeField] private TileBase statusTileYellow;
//     [SerializeField] private TileBase statusTileGreen;
//     [SerializeField] private GameObject registIcon;
//     [SerializeField] private GameObject lockIcon;
//     [SerializeField] private GameObject virusIcon;
//     [SerializeField] private Transform parent;
//     [SerializeField] private int gridWidth = 3;
//     [SerializeField] private int gridHeight = 3;
//     private Transform iconContainer;

//     void Start()
//     {
//         iconContainer = new GameObject("StatusIcons").transform;
//         iconContainer.SetParent(parent, false);
//         DrawStatus();
//         var data = new List<StatusObject>
//         {
//             new (Objects.Lock),
//             new (Objects.Lock),
//             new (Objects.Lock),
//             new (Objects.Virus),
//             new (Objects.Resist),
//         };
//         GameManager.Instance.statusObjectList = data;
//         DrawStatusObject();
//     }

//     private void DrawStatus()
//     {
//         var xAdjustment = 6;
//         var yAdjustment = 2;
//         for (int y = 0; y < gridHeight; y++)
//         {
//             for (int x = 0; x < gridWidth; x++)
//             {
//                 var pos = new Vector3Int(x + xAdjustment, y + yAdjustment, 0);
//                 tilemap.SetTile(pos, statusTileYellow);
//             }
//         }
//     }
//     public void DrawStatusObject()
//     {
//         foreach (Transform child in iconContainer)
//         {
//             Destroy(child.gameObject);
//         }
//         var data = GameManager.Instance.statusObjectList;
//         var groupedStatusObject = data.GroupBy(d => d.type);
//         foreach (var statusObject in groupedStatusObject)
//         {
//             switch (statusObject.Key)
//             {
//                 case Objects.Lock:
//                     InstantiateStatusObject(lockIcon, new Vector3Int(-1, 0), statusObject.ToList());
//                     break;
//                 case Objects.Virus:
//                     InstantiateStatusObject(virusIcon, new Vector3Int(-1, -1), statusObject.ToList());
//                     break;
//                 case Objects.Resist:
//                     InstantiateStatusObject(registIcon, new Vector3Int(-1, 1), statusObject.ToList());
//                     break;
//             }
//         }
//     }
//     private void InstantiateStatusObject(GameObject icon, Vector3Int baseVector, List<StatusObject> data)
//     {
//         int index = 0;
//         foreach (var item in data)
//         {
//             Vector3 basePos = tilemap.CellToWorld(baseVector + new Vector3Int(index, 0) + new Vector3Int(8, 4));
//             Instantiate(icon, basePos - new Vector3(0.5f, 0.5f), Quaternion.identity, iconContainer);
//             index++;
//         }
//     }
// }
