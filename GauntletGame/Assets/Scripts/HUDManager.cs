using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] int currentScore = 0;
    [SerializeField] int finalScore = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI axesText;
    [SerializeField] TextMeshProUGUI finalScoreText;

   
    // Start is called before the first frame update
    void Start()
    {
        finalScore = FindObjectOfType<GameManager>().getFinalScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = currentScore.ToString();
        finalScoreText.text = finalScore.ToString();
        healthText.text = FindObjectOfType<Player>().GetHealth().ToString();
        axesText.text = FindObjectOfType<Player>().GetAxes().ToString();
        
        
    }
    
    public void AddToScore(int scoreValue)
    {
        currentScore += scoreValue;
    }

    public int getScore()
    {
        return currentScore;
    }
}

