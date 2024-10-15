using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 15f;
    [SerializeField] float timeToShowCorrectAnswer = 2f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;
    
    float timerValue;
    void Update()
    {
        updateTimer();
    }

    public void cancelTimer()
    {
        timerValue = 0;
    }
    void updateTimer()
    {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = false;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
