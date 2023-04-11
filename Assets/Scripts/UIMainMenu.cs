using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject inputNameMenu;
    [SerializeField] GameObject nameNotAvailableMenu;
    [SerializeField] TMP_InputField inputFieldName;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void InputName()
    {
        startMenu.SetActive(false);
        inputNameMenu.SetActive(true);
    }

    public void StartGame()
    {
        //if (inputFieldName.text != null)
        //{            
        //    if (scoreKeeper.CanAddPlayerName(inputFieldName.text))
        //    {
        //        scoreKeeper.AddPlayerName(inputFieldName.text);
        //    }
        //    else
        //    {
        //        inputNameMenu.SetActive(false);
        //        nameNotAvailableMenu.SetActive(true);
        //        return;                
        //    }
        //}

        scoreKeeper.AddPlayerName(inputFieldName.text);
        levelManager.LoadGame();
    }

    public void BackToMenu()
    {
        nameNotAvailableMenu.SetActive(false);
        startMenu.SetActive(true);
    }
}
