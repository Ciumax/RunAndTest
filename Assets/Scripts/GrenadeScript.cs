using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private float radius = 10.0F;
    private float power = 400.0F;
    
  
    void OnCollisionEnter(Collision collision)
    {
        
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if(hit.tag == "Enemy")
            hit.SendMessage("AddDamage",5);

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
        Rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
        
    }
}
