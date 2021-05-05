using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 3f;
    [SerializeField] private GameObject _spawnedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(_spawnTime));
    }

    IEnumerator Spawn(float delayTime)
    {   
        while (true)
        {
            GameObject enemy = ObjectPool.SharedInstance.GetPooledObject(_spawnedObject.tag);
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
    }
}
