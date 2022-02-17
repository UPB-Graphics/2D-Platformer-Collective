using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text scoreText;
    int score;

    private void OnEnable()
    {
        // load last score value
        score = PlayerPrefs.GetInt("Score");
    }

    private void OnDisable()
    {
        // save current score 
        PlayerPrefs.SetInt("Score", score);
    }


    void Update()
    {
        // add 10 to score
        if (Input.GetKeyDown(KeyCode.Y))
        {
            score += 10;
        }

        // update score text
        _updateScore();

    }

    private void _updateScore()
    {
        scoreText.text = $"Score: {score}";
    }
}
