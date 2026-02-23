using UnityEngine;
using System.Collections;
using Assets.Code.Weapon;

namespace Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 2.0f;
        [SerializeField] private float _force = 4.0f;

        private Rigidbody _rb;

        public float Force
        {
            get
            {
                if (_force <= 0)
                {
                    return 0;
                }
                return _force;
            }

            set
            {
                _force = value;
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);

            if (other.collider.TryGetComponent<HealthController>(out HealthController health))
            {
                if (health.CanTakeDamage(_damage))
                {
                    return;
                }

                if (other.collider.TryGetComponent<Rigidbody>(out Rigidbody rb) == false)
                {
                    rb = other.gameObject.AddComponent<Rigidbody>();
                }

                rb.AddForce(_rb.linearVelocity * Force, ForceMode.Impulse);
            }
        }

        public void Sleep()
        {
            _rb.Sleep();
            gameObject.SetActive(false);
        }

        public void Run(Vector3 path, Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            _rb.WakeUp();
            _rb.AddForce(path);
        }
    }
}