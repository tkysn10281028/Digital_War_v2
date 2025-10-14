using DigitalWar.Project.Explore.Domain.Field.IF;
using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Field
{
    public class FieldDrawer : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour _drawFieldHandlerComponent;
        private IFieldDrawHandler drawFieldHandler;
        void Awake()
        {
            drawFieldHandler = _drawFieldHandlerComponent as IFieldDrawHandler;
        }
        void Start()
        {
            drawFieldHandler.Draw();
        }
    }

}
