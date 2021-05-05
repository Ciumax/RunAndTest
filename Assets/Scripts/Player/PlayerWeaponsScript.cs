using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsScript : MonoBehaviour
{
    public float grenadeRate = 3f;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _grenade;
    [SerializeField] private GameObject _turretPoint;

    private float _nextFire = 0;
    private float _nextGrenade = 0;
    private float _fireRate = 0.2f;
    private float _bulletSpeed = 450f;
    private float _grenadeSpeed = 600f;

    // Update is called once per frame
    void Update()
    {
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
}
