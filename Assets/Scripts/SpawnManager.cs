using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    private int enemiesInScene;
    public int timeToSpawn = 5;
    // Start is called before the first frame update
    void Start()
    {
        /*

        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemies[i], GenerateRandomPos(), enemies[i].transform.rotation);
        }
        */

        InvokeRepeating("SpawnWaveEnemy", 0, timeToSpawn);
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        enemiesInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesInScene < 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Instantiate(enemies[i], GenerateRandomPos(), enemies[i].transform.rotation);
            }
        }
        */

    }

    Vector3 GenerateRandomPos()
    {
        float randX = Random.Range(-12, 12);
        float randZ = Random.Range(-6, 6);

        Vector3 randomPos = new Vector3(randX, 10, randZ);
        return randomPos;
        
    }

    void SpawnWaveEnemy()
    {
        for (int i = 0; i < 3; i++)
            {
                Instantiate(enemies[i], GenerateRandomPos(), enemies[i].transform.rotation);
            }
    }
}
