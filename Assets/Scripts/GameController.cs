using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float scoreValue;
    public float escapeValue;
    public float rageOverTime;
    public int maxEnemies;

    public GameObject enemyPrefab;
    public Vector3 spawnMin;
    public Vector3 spawnMax;

    private float rage;
    private int goals;
    private int escapes;

    void Start()
    {
        rage = 0;
        goals = 0;
        escapes = 0;
        InvokeRepeating("SpawnEnemies", 1.0f, 6.0f); //make to increase over time later !
    }

    void Update()
    {
        if (rage > 1000.0f) GameOver();
        rage += rageOverTime * Time.deltaTime;
    }

    public void Score(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            rage += scoreValue;
            escapes++;
        }
    }

    public void Escape(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            rage += escapeValue;
            escapes++;
        }
    }

    void SpawnEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            float x = Random.Range(spawnMin.x, spawnMax.x);
            float y = Random.Range(spawnMin.y, spawnMax.y);
            float z = Random.Range(spawnMin.z, spawnMax.z);

            Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
        }
    }

    void GameOver() 
    {
        //handle game over
    }

    public float GetRage()
    {
        return rage;
    }

    public float GetGoals()
    {
        return goals;
    }

    public float GetEscapes()
    {
        return escapes;
    }
}
