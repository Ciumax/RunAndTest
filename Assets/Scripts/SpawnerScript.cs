using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    private float spawnTime = 3f;
    [SerializeField]
    GameObject spawnedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn(spawnTime));
    }

    IEnumerator Spawn(float delayTime)
    {
        
        while (true)
        {
            GameObject enemy = ObjectPool.SharedInstance.GetPooledObject(spawnedObject.tag);
            enemy.transform.position = transform.position;
            enemy.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
    }
    
}
