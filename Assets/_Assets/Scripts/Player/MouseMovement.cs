using UnityEngine;
using UnityEngine.InputSystem;
public class MouseMovement : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] float _mouseSensivity = 100f;
    [SerializeField] float _minRotation = -80f;
    [SerializeField] float _maxRotation = 80f;

    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        float mouseX = delta.x * _mouseSensivity;
        float mouseY = delta.y * _mouseSensivity;

        _xRotation -= mouseY;
        _yRotation += mouseX;

        _xRotation = Mathf.Clamp(_xRotation, _minRotation, _maxRotation);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, _yRotation, 0f);

    }

}
