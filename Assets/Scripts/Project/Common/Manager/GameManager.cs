using DigitalWar.Project.Common.Objects.Explore;
using DigitalWar.Project.Explore.Domain.Player;
using UnityEngine;
namespace DigitalWar.Project.Common.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public ExploreObject ExploreObject = new();
        public PlayerCurrentPosition PlayerCurrentPosition = new();
        public bool IsPlayerLocked { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LockPlayer()
        {
            IsPlayerLocked = true;
        }

        public void UnlockPlayer()
        {
            IsPlayerLocked = false;
        }
    }
}
