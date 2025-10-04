using System;
using UnityEngine;


public class GameInput : MonoBehaviour
{
    public event Action OnJump;
    private InputActions _action;

    private void Awake()
    {
        _action = new InputActions();
        _action.Player.Enable();

        _action.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke();
    }

    public Vector3 GetMovementNormalized(Transform transform)
    {
        Vector3 input = _action.Player.Move.ReadValue<Vector3>();
        //Debug.Log(input.normalized);
        return transform.TransformDirection(input.normalized);
    }

}
