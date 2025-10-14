using System.Collections.Generic;
using System.Linq;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Manager;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace DigitalWar.Project.Explore.Domain.Status.ObjectDraw
{
    public class StatusObjectDrawer : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private GameObject _registIcon;
        [SerializeField] private GameObject _lockIcon;
        [SerializeField] private GameObject _virusIcon;
        [SerializeField] private Transform _parent;
        private Transform iconContainer;

        void Awake()
        {
            iconContainer = new GameObject("StatusIcons").transform;
            iconContainer.SetParent(_parent, false);
        }

        public void DrawStatusObject()
        {
            foreach (Transform child in iconContainer)
            {
                Destroy(child.gameObject);
            }

            var data = GameManager.Instance.ExploreObject.StatusObjectList;
            var groupedStatusObject = data.GroupBy(d => d.Type);
            foreach (var statusObject in groupedStatusObject)
            {
                switch (statusObject.Key)
                {
                    case Objects.Lock:
                        InstantiateStatusObject(_lockIcon, new Vector3Int(-1, 0), statusObject.ToList());
                        break;
                    case Objects.Virus:
                        InstantiateStatusObject(_virusIcon, new Vector3Int(-1, -1), statusObject.ToList());
                        break;
                    case Objects.Resist:
                        InstantiateStatusObject(_registIcon, new Vector3Int(-1, 1), statusObject.ToList());
                        break;
                }
            }
        }

        private void InstantiateStatusObject(GameObject icon, Vector3Int baseVector, List<StatusObject> data)
        {
            int index = 0;
            foreach (var item in data)
            {
                Vector3 basePos = _tilemap.CellToWorld(baseVector + new Vector3Int(index, 0) + new Vector3Int(8, 4));
                Instantiate(icon, basePos - new Vector3(0.5f, 0.5f), Quaternion.identity, iconContainer);
                index++;
            }
        }
    }
}