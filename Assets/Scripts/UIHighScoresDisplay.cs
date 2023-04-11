using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIHighScoresDisplay : MonoBehaviour
{
    [SerializeField] GameObject highScores;

    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        FillHighScores();
    }

    void FillHighScores()
    {
        var previousHighScores = scoreKeeper.GetPreviousHighScores();

        if (previousHighScores.Any())
        {
            int index = 0;

            foreach (var item in previousHighScores)
            {
                var pos = index;
                highScores.GetComponentsInChildren<TextMeshProUGUI>()[index].text = pos + 1 + ". " + item.Key + " - " + item.Value;
                index++;
            }
        }
    }
}
