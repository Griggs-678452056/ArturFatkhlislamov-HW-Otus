using UnityEngine;
using System.Collections;

namespace Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Rocket : MonoBehaviour
    {
        private const int COLLISION_SIZE = 128;

        [SerializeField] private float _powerExplosion;
        [SerializeField] private float _scale;

        private Rigidbody _rb;
        private readonly Collider[] _collidedObjects = new Collider[COLLISION_SIZE];

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();            
        }

        private void OnCollisionEnter(Collision other)
        {
            Explosion explosion = new GameObject().AddComponent<Explosion>();
            explosion.transform.position = transform.position;

            Destroy(gameObject);

            float radius = _scale * 0.5f;
            Vector3 center = other.contacts[0].point;
            int countCollied = Physics.OverlapSphereNonAlloc(center, radius, _collidedObjects);

            for (int i = 0; i < countCollied; i++)
            {
                Collider collidedObject = _collidedObjects[i];

                if (collidedObject.TryGetComponent(out HealthController healthController))
                {
                    if (healthController.CanTakeDamage(healthController.MaxHP))
                    {
                        return;
                    }
                    if (healthController.TryGetComponent(out Rigidbody rb) == false)
                    {
                        rb = healthController.gameObject.AddComponent<Rigidbody>();
                    }
                    rb.AddExplosionForce(_powerExplosion, center, radius);
                }
            }
        }

        public void Run(Vector3 path)
        {
            transform.SetParent(null);
            _rb.WakeUp();
            _rb.isKinematic = false;
            _rb.AddForce(path, ForceMode.Impulse);
        }

        public void Sleep(Vector3 startPoint)
        {
            _rb.Sleep();
            _rb.isKinematic = true;
            transform.position = startPoint;
        }
    }
}