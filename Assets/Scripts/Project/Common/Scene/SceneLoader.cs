using DigitalWar.Project.Common.Enums;
using UnityEngine.SceneManagement;

namespace DigitalWar.Project.Common.Scene
{
    public static class SceneLoader
    {
        public static void Load(Scenes scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
    }
}
