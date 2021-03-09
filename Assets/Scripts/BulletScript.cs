using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
            Rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

}
