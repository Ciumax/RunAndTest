using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public float grenadeRate = 3f; /// <summary>
    /// ????
    /// </summary>

    private float _radius = 10.0F;
    private float _power = 400.0F;
    
    private void OnEnable()
    {
        StartCoroutine(Reload(this.gameObject, grenadeRate));
    }
    void OnCollisionEnter(Collision collision)
    {   
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders)
        {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(hit.tag == "Enemy")
                hit.SendMessage("AddDamage",5);
            if (rb != null)
                rb.AddExplosionForce(_power, explosionPos, _radius, 3.0F);
        }
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
        Rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }
    IEnumerator Reload(GameObject ammo, float delayTime)
    {
        Rigidbody Rb;
        yield return new WaitForSeconds(delayTime);
        Rb = ammo.GetComponent<Rigidbody>();
        Rb.velocity = Vector3.zero;
        ammo.SetActive(false);
    }
}
