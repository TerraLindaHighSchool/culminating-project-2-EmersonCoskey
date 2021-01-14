using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    private GameController gameController;

    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);

        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    public void SetDifficulty()
    {
        //Debug.Log(gameObject.name + "was clicked");
        gameController.StartGame(difficulty);
    }
}