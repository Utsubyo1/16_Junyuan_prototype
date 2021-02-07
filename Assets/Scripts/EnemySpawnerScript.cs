using System.Collections;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject EnemyPrefab;


    public float spawnInterval;
    public float enemycount;

    //Spawn area
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpawn(spawnInterval));
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
           
            
           
        }
    }
  
}
