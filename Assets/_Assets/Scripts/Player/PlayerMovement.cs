using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameInput")]
    [SerializeField] GameInput _input;

    [Header("Setting player")]
    [SerializeField] float _speed = 10f;
    [SerializeField] float _jumpHeight = 10f;
    [SerializeField] float _smooth = 5f;
    

    [Header("Ground")]
    [SerializeField] Transform _checkGroundTransform;
    [SerializeField] LayerMask _groundLayerMark;
    [SerializeField] float _groundDistance = 0.2f;

    private Vector3 _currentMove;
    private Vector3 _velocity;
    private float _gravity = -9.81f * 2;
    private bool _isGrounded = false;


    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _input.OnJump += HandleInputJump;
    }

    private void OnDisable()
    {
        _input.OnJump -= HandleInputJump;
    }

    private void Update()
    {
        CheckGround();

        if (_isGrounded && _velocity.y < 0) _velocity.y = -2f;

        Vector3 input = _input.GetMovementNormalized(transform);
        HandleInputMove(input);
        OnAnimation(input);
    }

    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(_checkGroundTransform.position , _groundDistance, _groundLayerMark);
    }

    private void HandleInputMove(Vector3 input)
    {
        Vector3 moveTarget = input * _speed;
        _currentMove = Vector3.Lerp(_currentMove, moveTarget, _smooth * Time.deltaTime);
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move((_velocity + _currentMove) * Time.deltaTime);
    }

    private void HandleInputJump()
    {
        if(_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_gravity * -2f * _jumpHeight);
        }    
    }    

    private void OnAnimation(Vector3 input)
    {
        //Debug.Log(input);
    }
}
