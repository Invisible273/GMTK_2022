using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public int score;
    private Text ScoreText;

    void Start()
    {
        score = 0;

        ScoreText = GetComponent<Text>();
        ScoreText.text = score.ToString();
    }

        
    void Update()
    {
        ScoreText.text = score.ToString();
    }

    public void IncrementScore()
    {
        score++;
    }

}

