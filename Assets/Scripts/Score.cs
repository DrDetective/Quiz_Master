using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int correctQuestions = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctQuestions;
    }
    public void IncrementCAns()
    {
        correctQuestions++;
    }

    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }
    public void IncrementQS()
    {
        questionsSeen++;
    }
    public int Calculate()
    {
        return Mathf.RoundToInt(correctQuestions / (float)questionsSeen * 100);
    }
}
