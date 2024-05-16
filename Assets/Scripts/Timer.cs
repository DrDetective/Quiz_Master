using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float TimetoComplete = 30f;
    [SerializeField] float TimeforCorrectAns = 10f;
    float timerValue;
    public bool LoadNextQuestion;
    public bool isAnswered = false;
    public float FillFraction;
    void Update()
    {
        UpdateTimer();
    }
    public void SkipTimer()
    { 
        timerValue = 0;
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (isAnswered)
        {
            if (timerValue > 0) 
            {
                FillFraction = timerValue / TimeforCorrectAns;
            }
            else
            {
                timerValue = TimeforCorrectAns;
                isAnswered = false;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                FillFraction = timerValue / TimetoComplete;
            }
            else
            {
                timerValue = TimetoComplete;
                isAnswered = true;
                LoadNextQuestion = true;
            }
        }
        Debug.Log($"{isAnswered}: {timerValue} = {FillFraction}");
    }
}
