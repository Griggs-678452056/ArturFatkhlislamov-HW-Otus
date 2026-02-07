using UnityEngine;

namespace Code
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _rotateSpeed = 120f;

        private Rigidbody rb;
        private float moveInput;
        private float rotateInput;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Debug.Log("Золотые кубики - аптечки");
        }

        private void Update()
        {
            moveInput = Input.GetAxis("Vertical");
            rotateInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            Vector3 moveDirection = transform.forward * moveInput * _speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + moveDirection);

            Quaternion rotation = Quaternion.Euler(0f, rotateInput * _rotateSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
}