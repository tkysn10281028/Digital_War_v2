using DigitalWar.Project.Common.MapName;
using DigitalWar.Project.Explore.Domain.Field.IF;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Field
{
    public class FieldDrawer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _fieldDrawHandlerComponent;
        private IFieldDrawHandler fieldDrawHandler;

        void Awake()
        {
            fieldDrawHandler = _fieldDrawHandlerComponent as IFieldDrawHandler;
        }

        void Start()
        {
            fieldDrawHandler.Draw(MapNameResolver.Resolve());
        }

        public void RedrawField(string fileName)
        {
            fieldDrawHandler.Draw(fileName);
        }
    }
}
