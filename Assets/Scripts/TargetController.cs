using UnityEngine;

namespace Bounce
{
    public class TargetController : MonoBehaviour
    {
        public int bonus = 2;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out HitController player) && player.Pickup(bonus))
            {
                GameController.Instance.Spawn();
                GUIController.Instance.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
