using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float scoreValue;
    public float escapeValue;
    public float rageOverTime;
    public int maxEnemies;

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public Vector3 spawnMin;
    public Vector3 spawnMax;

    public GameObject menu;
    public GameObject hud;
    public GameObject gameOver;

    private TextMeshProUGUI goalsText;
    private TextMeshProUGUI escapeText;
    private RectTransform rageBarTransform;

    private float rage;
    private int goals;
    private int escapes;
    private bool isGameActive;
    private PlayerController playerController;

    void Start()
    {
        rage = 0;
        goals = 0;
        escapes = 0;

        goalsText = GameObject.Find("Goal Count").GetComponent<TextMeshProUGUI>();
        escapeText = GameObject.Find("Escape Count").GetComponent<TextMeshProUGUI>();
        rageBarTransform = GameObject.Find("Rage Bar").GetComponent<RectTransform>();
        playerController = GameObject.Find("Referee").GetComponent<PlayerController>();
        playerController.gameObject.SetActive(false);

        hud.SetActive(true);
    }

    void Update()
    {
        if (isGameActive)
        {
            if (rage > 1000.0f) GameOver();
            rage += rageOverTime * Time.deltaTime;
            rage = Mathf.Clamp(rage, 0.0f, 1000.0f);
            rageBarTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, rage / 2);
        }
    }

    public void Score(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Enemy enemyController = enemy.GetComponent<Enemy>();
            enemyController.Die();
            rage += scoreValue;
            goals++;

            goalsText.text = "Goals: " + goals;
        }
    }

    public void Escape(GameObject enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Destroy(enemy);
            rage += escapeValue;
            escapes++;

            escapeText.text = "Escapes: " + escapes;
        }
    }

    void SpawnEnemies()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length <= maxEnemies)
        {
            float x = UnityEngine.Random.Range(spawnMin.x, spawnMax.x);
            float y = UnityEngine.Random.Range(spawnMin.y, spawnMax.y);
            float z = UnityEngine.Random.Range(spawnMin.z, spawnMax.z);

            if (UnityEngine.Random.Range(0, 5) > 0)
            {
                Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 180, 0));
            }
            else if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0)
            {
                Instantiate(powerupPrefab, new Vector3(x, y, z), Quaternion.Euler(0, 0, 0));
            }
        }
    }

    void GameOver()
    {
        isGameActive = false;
        hud.SetActive(false);
        gameOver.SetActive(true);
        playerController.Die();
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(float spawnInterval)
    {
        isGameActive = true;
        InvokeRepeating("SpawnEnemies", 1.0f, spawnInterval); //make to increase over time later !
        menu.SetActive(false);
        hud.SetActive(true);

        playerController.gameObject.SetActive(true);
    }
}
