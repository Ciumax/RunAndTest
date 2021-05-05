using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float health = 10;
    public Text healthText;
    public float grenadeRate = 3f;

    [SerializeField] private float _speed = 6f;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private GameObject _turretPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _grenade;

    private float _turnSmoothTime = 0.1f;
    private float _nextFire = 0;
    private float _nextGrenade = 0;
    private float _fireRate = 0.2f;
    private float _bulletSpeed = 450f;
    private float _grenadeSpeed = 600f;
    private float _turnSmoothVelocity;
    private float _verticalVelocity;
    private float _gravity = 20f;
    private float _jumpForce = 10f;
    private bool _isGrounded;
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

        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            Fire(transform.forward, _bullet, _bulletSpeed);
        }

        if (Input.GetButton("Fire2") && Time.time > _nextGrenade)
        {
            _nextGrenade = Time.time + grenadeRate;
             Vector3 throwVector;
             throwVector = new Vector3(0.0f, 0.4f, 0.0f);
             throwVector = throwVector + transform.forward;
             Fire(throwVector, _grenade, _grenadeSpeed);
        }
    }

    void Fire(Vector3 TrajectoryVector, GameObject AmmoType, float AmmoSpeed)
    {
        GameObject Ammo = ObjectPool.SharedInstance.GetPooledObject(AmmoType.name);
        if (Ammo != null)
        {
            Ammo.transform.position = _turretPoint.transform.position;
            Ammo.transform.rotation = transform.rotation;
            Ammo.SetActive(true);
            Ammo.GetComponent<Rigidbody>().AddForce(TrajectoryVector * AmmoSpeed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            collision.gameObject.SetActive(false);
            healthText.SendMessage("SetHp");
        }
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
