using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private GameObject _tagTarget;

    private void OnEnable()
    {
        StartCoroutine(Reload(this.gameObject, 5));
    }
        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == _tagTarget.tag)
        {
            Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
            Rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
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
}
