using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 6f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform mainCamera;
    [SerializeField] private GameObject turret;
    public float HP = 10 ;
    public Text healthText;
    private float turnSmoothTime = 0.1f;
    private float nextFire = 0;
    private float nextGrenade = 0;
    private float fireRate = 0.2f;
    public float grenadeRate = 3f;
    private float bulletSpeed = 450f;
    private float bulletLifeTime = 4f;
    private float grenadeLifeTime = 6f;
    private float grenadeSpeed = 600f;
    private float turnSmoothVelocity;
    private float verticalVelocity;
    private float gravity = 20f;
    private float jumpForce = 10f;
    private bool isGrounded;
    void Update()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float vercitalMove = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontalMove, 0f, vercitalMove).normalized;

       
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
                isGrounded = false;
            }

        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 jumpVector = new Vector3(0, verticalVelocity, 0);
        controller.Move(jumpVector * Time.deltaTime);

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }

        if (Input.GetButton("Fire2") && Time.time > nextGrenade)
        {
            nextGrenade = Time.time + grenadeRate;
            Throw();
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");
        if (bullet != null)
        {
            bullet.transform.position = turret.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            StartCoroutine(Reload(bullet, bulletLifeTime));
        }

    }
    void Throw()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Grenade");
        if (bullet != null)
        {
            Vector3 throwVector;
            throwVector = new Vector3(0.0f, 0.4f, 0.0f);
            throwVector = throwVector + transform.forward;
            bullet.transform.position = turret.transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(throwVector * grenadeSpeed) ;
            StartCoroutine(Reload(bullet, grenadeLifeTime));
        }

    }

    IEnumerator Reload(GameObject ammo, float delayTime)
    {
        Rigidbody Rb;
        yield return new WaitForSeconds(delayTime);
        Rb = ammo.GetComponent<Rigidbody>();
        Rb.velocity = Vector3.zero;
        ammo.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HP--;
            collision.gameObject.SetActive(false);
            healthText.SendMessage("SetHp");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
       
    }
}
