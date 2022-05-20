using System;
using UnityEngine;

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
        private float _time;

        public float maxForce = 5f;
        public float sliderSpeed = 1f;
        public int startingShots = 5;
        public float stopTolerance = 0.005f;
        public float startTime = 120.0f;

        private void Awake()
        {
            _stopToleranceSqr = stopTolerance * stopTolerance;
            _rigidbody = gameObject.GetComponent<Rigidbody>();

            switch (GameController.Instance.mode)
            {
                case GameMode.Precision:
                    _shotsRemaining = startingShots;
                    GUIController.Instance.UpdateLife(_shotsRemaining);
                    break;
                case GameMode.Speed:
                    _time = startTime;
                    GUIController.Instance.UpdateLife(_time);
                    break;
                default:
                    break;
            }
            
            GUIController.Instance.UpdateForce(_horizontalForce, _verticalForce);
            
        }

        private void Update()
        {
            if (_time > 0f)
            {
                _time -= Time.deltaTime;
                GUIController.Instance.UpdateLife(_time);
            }
        }

        private void FixedUpdate()
        {
            if (!_stopped && _rigidbody.velocity.sqrMagnitude <= _stopToleranceSqr)
            {
                _stopped = true;
                _rigidbody.velocity = Vector3.zero;
            }

            if (Fire && (_shotsRemaining > 0 || _time > 0))
            {
                Fire = false;
                _stopped = false;
                _shotsRemaining--;
                GUIController.Instance.UpdateLife(_shotsRemaining);
                Vector3 force = new Vector3(_horizontalForce, 0f, _verticalForce);
                _rigidbody.velocity = force;
                _verticalForce = 0f;
                _horizontalForce = 0f;
            }
        }

        private bool PrecisionPickup(int amount)
        {
            if (_stopped)
            {
                _shotsRemaining += amount;
                GUIController.Instance.UpdateLife(_shotsRemaining);
                return true;
            }
            return false;
        }

        public bool Pickup(int value)
        {
            return GameController.Instance.mode switch
            {
                GameMode.Precision => PrecisionPickup(value),
                GameMode.Speed => SpeedPickup(value),
                _ => false
            };
        }

        private bool SpeedPickup(int value)
        {
            if (_time > 0f)
            {
                _time += value * 3f;
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
