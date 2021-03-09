using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
   
    private Transform destination;
    private NavMeshAgent navMeshAgent;
    public int Hp = 10;
    public int Mp = 11;
    [SerializeField]
    private float followAgainTime = 0.1f;

    private void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(SetDestination(followAgainTime));
        Debug.Log("RUSZAM " + this);
    }

    private void OnEnable()
    {
        destination = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        StartCoroutine(SetDestination(followAgainTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Hp--;
            if (Hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }

    }
    IEnumerator SetDestination(float delayTime)
    {
        while (true)
        {
            
            Vector3 targetVector = destination.transform.position;
            navMeshAgent.SetDestination(targetVector);
            yield return new WaitForSeconds(delayTime);
        }
    }
    void AddDamage(int dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
