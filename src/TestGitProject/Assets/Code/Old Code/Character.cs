using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Rigidbody _rb;
        private Vector3 _input;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            _input = new Vector3(h, 0, v);
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = _input * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_rb.position + moveDirection);
        }
    }
}