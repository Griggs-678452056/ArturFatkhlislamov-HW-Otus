using System.Collections;
using UnityEngine;

namespace Code
{
    public class MouseLook : MonoBehaviour
    {

        [SerializeField] private float _mouseSensivity = 150f;
        [SerializeField] private Transform _cameraPivot;

        private float _xRotation = 0f;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);
            transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

            _cameraPivot.Rotate(Vector3.up * mouseX);
        }
    }
}