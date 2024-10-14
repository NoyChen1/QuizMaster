using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Qustions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Score")]
    [SerializeField]TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;


    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }

    private void Update()
    {
        changeTimerFiller();
        loadNextQuestion();
    }

    void loadNextQuestion()
    {
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            displayAnswer(-1);
            setButtonsState(false);
        }
    }
    void changeTimerFiller()
    {
        timerImage.fillAmount = timer.fillFraction;
    }
    void getNextQuestion()
    {
        if(questions.Count > 0)
        {
            setButtonsState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuestion();
            scoreKeeper.incrementQuestionsSeen();
        }
    }

    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }
    void displayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        initAnswers();
    }
    void initAnswers()
    {
        TextMeshProUGUI buttonText;

        for (int i = 0; i < answerButton.Length; i++)
        {
            buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        displayAnswer(index);
        setButtonsState(false);
        timer.cancelTimer();
        scoreText.text = "Score: " + scoreKeeper.calculateScore() + "%";
    }

    void displayAnswer(int index)
    {
        Image buttonImage;
        
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct !";
            buttonImage = answerButton[index].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string corrextAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, The Correct Answer was:\n" + corrextAnswer;
            buttonImage = answerButton[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }
    void setButtonsState(bool state)
    {
        Button button;
        for (int i = 0; i < answerButton.Length; i++)
        {
            button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void setDefaultButtonSprites()
    {
        Image buttonImage;
        for(int i = 0; i < answerButton.Length; i++)
        {
            buttonImage = answerButton[i].GetComponentInChildren<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
