using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI WinText;
    Score score;
    void Start()
    {
        score = FindObjectOfType<Score>();
    }
    public void ShowScore()
    {
        WinText.text = $"Congrats!\nYou got {score.Calculate()}%";
    }
}
