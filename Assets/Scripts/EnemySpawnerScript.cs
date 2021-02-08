using System.Collections;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject EnemyPrefab;


    public float spawnInterval;
     int enemycount;

    //Spawn area
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    bool stopspawn = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndSpawn(spawnInterval));
        PlayerScript.Playerdeath = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemycount == 5)
        {
            stopspawn = true;
        }
    }
    
    private IEnumerator WaitAndSpawn(float waitTime)
    {
        enemycount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemycount >= 5)
        {
            stopspawn = true;
        }
        if (stopspawn == false)
        {
            while (PlayerScript.Playerdeath == false)
            {
                yield return new WaitForSeconds(waitTime);

                Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
                Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);



            }
        }
    }
  
}
