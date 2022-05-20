using UnityEngine;

namespace Bounce
{
    public class TargetController : MonoBehaviour
    {
        public int bonusShots = 2;

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out HitController player) && player.AddShots(bonusShots))
            {
                GameController.Instance.Spawn();
                GUIController.Instance.AddScore(1);
                Destroy(gameObject);
            }
        }
    }
}
