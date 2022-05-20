using UnityEngine;
using TMPro;

namespace Bounce
{
    public class HitController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private float _horizontalForce = 0f;
        private float _verticalForce = 0f;
        public bool Fire { set; private get; }
        private bool _stopped;
        private int _shotsRemaining;
        private float _stopToleranceSqr;

        public float maxForce = 5f;
        public float sliderSpeed = 1f;
        public int startingShots = 5;
        public float stopTolerance = 0.005f;

        private void Awake()
        {
            _stopToleranceSqr = stopTolerance * stopTolerance;
            _rigidbody = gameObject.GetComponent<Rigidbody>();
            _shotsRemaining = startingShots;
            GUIController.Instance.UpdateForce(_horizontalForce, _verticalForce);
            GUIController.Instance.UpdateShots(_shotsRemaining);
        }

        private void FixedUpdate()
        {
            if (!_stopped && _rigidbody.velocity.sqrMagnitude <= _stopToleranceSqr)
            {
                _stopped = true;
                _rigidbody.velocity = Vector3.zero;
            }

            if (Fire && _shotsRemaining > 0)
            {
                Fire = false;
                _stopped = false;
                _shotsRemaining--;
                GUIController.Instance.UpdateShots(_shotsRemaining);
                Vector3 force = new Vector3(_horizontalForce, 0f, _verticalForce);
                _rigidbody.velocity = force;
                _verticalForce = 0f;
                _horizontalForce = 0f;
            }
        }

        public bool AddShots(int amount)
        {
            if (_stopped)
            {
                _shotsRemaining += amount;
                GUIController.Instance.UpdateShots(_shotsRemaining);
                return true;
            }
            return false;
        }
        

        public void UpdateForce(float vertical, float horizontal)
        {
            _horizontalForce = !Mathf.Approximately(horizontal, 0f) &&
                               !Mathf.Approximately(_horizontalForce, -maxForce) &&
                               !Mathf.Approximately(_horizontalForce, maxForce)
                ? _horizontalForce += horizontal * sliderSpeed * Time.deltaTime
                : 0f;

            _verticalForce = !Mathf.Approximately(vertical, 0f) &&
                             !Mathf.Approximately(_verticalForce, -maxForce) &&
                             !Mathf.Approximately(_verticalForce, maxForce)
                ? _verticalForce += vertical * sliderSpeed * Time.deltaTime
                : 0f;

            _verticalForce = Mathf.Clamp(_verticalForce, -maxForce, maxForce);
            _horizontalForce = Mathf.Clamp(_horizontalForce, -maxForce, maxForce);
            GUIController.Instance.UpdateForce(_horizontalForce, _verticalForce);
        }
    }
}
