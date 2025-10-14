using UnityEngine;

namespace DigitalWar.Project.Explore.Domain.Player.Rotate
{
    public class PlayerRotator : MonoBehaviour
    {
        public Quaternion Rotate(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}