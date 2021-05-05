using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{


    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private float _speed = 6f;

    private float _turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _verticalVelocity;
    private float _gravity = 20f;
    private float _jumpForce = 10f;
    private bool _isGrounded;
    void Start()
    {

    }

    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float vercitalMove = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalMove, 0f, vercitalMove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir * _speed * Time.deltaTime);
        }

        if (_isGrounded)
        {
            _verticalVelocity = -_gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                _verticalVelocity = _jumpForce;
                _isGrounded = false;
            }
        }
        else
        {
            _verticalVelocity -= _gravity * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, _verticalVelocity, 0);
        _controller.Move(jumpVector * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        _isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _isGrounded = false;
    }
}
