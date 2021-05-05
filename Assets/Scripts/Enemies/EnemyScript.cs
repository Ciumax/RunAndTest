using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public int hp = 3;


    private Transform _destination;
    private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _followAgainTime = 0.1f;

    private void Start()
    {
        _destination = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(SetDestination(_followAgainTime));
    }
    private void OnEnable()
    {
        hp = 3;
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
        _destination = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(SetDestination(_followAgainTime));

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hp--;
            if (hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    IEnumerator SetDestination(float delayTime)
    {
        while (true)
        {

            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
            yield return new WaitForSeconds(delayTime);
        }
    }
    void AddDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
