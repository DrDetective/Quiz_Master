using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO Currentquestion;
    [Header("Answers/Buttons")]
    [SerializeField] GameObject[] Abuttons;
    [SerializeField] Sprite CorrectAnswerButton;
    [SerializeField] Sprite DefaultAnswerButton;
    [SerializeField] Image TimerImage;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] Slider ProgBar;
    Score score;
    Timer timer;
    bool hasAnsEarly = true;
    public bool Complete;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        score = FindObjectOfType<Score>();
        ProgBar.maxValue = questions.Count;
        ProgBar.value = 0;
    }
    private void Update()
    {
        TimerImage.fillAmount = timer.FillFraction;
        if (timer.LoadNextQuestion)
        {
            hasAnsEarly = false;
            GetNextQuestion();
            timer.LoadNextQuestion = false;
        }
        else if (!hasAnsEarly && !timer.isAnswered)
        {
            DisplayAnswers(-1);
            BtnState(false);
        }
    }
    public void AnswerSelected(int index)
    {
        hasAnsEarly = true;
        DisplayAnswers(index);
        BtnState(false);
        timer.SkipTimer();
        ScoreText.text = $"Score: {score.Calculate()}%";
        if (ProgBar.value == ProgBar.maxValue)
        {
            Complete = true;
        }
    }
    void DisplayAnswers(int index)
    {
        Image btnImage;
        if (index == Currentquestion.CorrectAnswer())
        {
            questionText.text = "Correct";
            btnImage = Abuttons[index].GetComponent<Image>();
            btnImage.sprite = CorrectAnswerButton;
            score.IncrementCAns();
        }
        else
        {
            questionText.text = $"It was {Currentquestion.GetAnswer(Currentquestion.CorrectAnswer())}";
            btnImage = Abuttons[Currentquestion.CorrectAnswer()].GetComponent<Image>();
            btnImage.sprite = CorrectAnswerButton;
        }
    }
    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            BtnState(true);
            SetDefaultBtnSprite();
            GetRandomQuestion();
            DisplayQuestion();
            ProgBar.value++;
            score.IncrementQS();
        }
    }
    private void DisplayQuestion()
    {
        questionText.text = Currentquestion.GetQuestion();
        for (int i = 0; i < Abuttons.Length; i++)
        {
            TextMeshProUGUI ansButtonsText = Abuttons[i].GetComponentInChildren<TextMeshProUGUI>();
            ansButtonsText.text = Currentquestion.GetAnswer(i);
        }
    }
    private void BtnState(bool state)
    {
        for (int i = 0;i < Abuttons.Length;i++)
        {
            Button btn = Abuttons[i].GetComponent<Button>();
            btn.interactable = state;
        }
    }
    private void SetDefaultBtnSprite()
    {
        for (int i = 0; i < Abuttons.Length; i++)
        {
            Image Imagebtn = Abuttons[i].GetComponent<Image>();
            Imagebtn.sprite = DefaultAnswerButton;
        }
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        Currentquestion = questions[index];
        if (questions.Contains(Currentquestion))
        {
            questions.Remove(Currentquestion);
        }
    }
}
