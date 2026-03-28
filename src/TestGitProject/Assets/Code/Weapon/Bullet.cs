using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage = 2.0f;
        [SerializeField] private float _force = 4.0f;

        public bool IsActive { get; private set; }

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
                if (IsActive == false)
                {
                    _force = 0;
                    return;
                }

                _force = value;
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            Sleep();

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

            if (other.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
            }

            if (other.collider.TryGetComponent<NPCHealth>(out NPCHealth npcHealth))
            {
                npcHealth.TakeDamage(_damage);
            }                                   
        }

        public void Sleep()
        {
            if (_rb == null)
            {
                return;
            }

            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.Sleep();

            gameObject.SetActive(false);
            IsActive = false;
        }

        public void Run(Vector3 path, Vector3 position)
        {
            if (_rb == null)
            {
                return;
            }

            transform.position = position;
            transform.parent = null;

            gameObject.SetActive(true);
            _rb.WakeUp();
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            _rb.AddForce(path);
            IsActive = true;
        }
    }
}