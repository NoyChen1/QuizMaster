using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    public void showFinalScore()
    {
        int finalScore = scoreKeeper.calculateScore();
        if (finalScore >= 50)
        {
            finalScoreText.text = "Congratulations !\n You Got a Score of: " 
                + finalScore + "%";
        }
        else
        {
            finalScoreText.text = "Sorry, You Got a Score of: " 
                + finalScore + "%\n Maybe Next Time";
        }
    }
}
