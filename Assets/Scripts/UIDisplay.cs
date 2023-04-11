using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;


    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Lives")]
    [SerializeField] GameObject livesCount;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        healthSlider.maxValue = playerHealth.GetCurrentHealth();
    }

    void Update()
    {
        healthSlider.value = playerHealth.GetCurrentHealth();
        scoreText.text = scoreKeeper.GetCurrentScore().ToString("000000000");
        DisplayLives();
    }

    void DisplayLives()
    {
        int currentLives = playerHealth.GetCurrentLives();
        var livesOnDisplay = livesCount.GetComponentsInChildren<Image>();

        int disableLiveCount = livesOnDisplay.Length - currentLives;
        for(int i = 0; i < disableLiveCount; i++)
        {
            livesOnDisplay[i].enabled = false;
        }
    }
}
