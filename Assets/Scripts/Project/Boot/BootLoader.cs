using DigitalWar.Project.Common;
using DigitalWar.Project.Common.Enums;
using DigitalWar.Project.Common.Scene;
using UnityEngine;

namespace DigitalWar.Project.Boot
{
    public class BootLoader : MonoBehaviour
    {
        void Start()
        {
            SceneLoader.Load(Scenes.ExploreScene);
        }
    }
}
