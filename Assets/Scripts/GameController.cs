using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float scoreValue;
    public float escapeValue;
    public float rageOverTime;

    public GameObject enemyPrefab;
    public Vector3 spawnMin;
    public Vector3 spawnMax;

    public float rage;
    private int goals;
    private int escapes;

    private void Start()
    {
        rage = 0;
        goals = 0;
        escapes = 0;
        InvokeRepeating("SpawnEnemies", 1.0f, 4.0f); //make to increase over time later !
    }

    private void Update()
    {
        if (rage > 1000.0f) GameOver();
        rage += rageOverTime * Time.deltaTime;
    }

    void Score(GameObject enemy)
    {
        if (enemy.CompareTag("enemy"))
        {
            rage += scoreValue;
            escapes++;
        }
    }

    void Escape(GameObject enemy)
    {
        if (enemy.CompareTag("enemy"))
        {
            rage += escapeValue;
            escapes++;
        }
    }

    void SpawnEnemies(/*int numToSpawn*/)
    {
        /*for (int i = 0; i < numToSpawn; i++)
        { */
            float x = Random.Range(spawnMin.x, spawnMax.x);
            float y = Random.Range(spawnMin.y, spawnMax.y);
            float z = Random.Range(spawnMin.z, spawnMax.z);

            Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
        /*}*/
    }

    void GameOver() 
    {
        //handle game over
    }
}
