using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;
    AudioPlayer audioPlayer;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();    
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        audioPlayer.PlayGameMusic();
        SceneManager.LoadScene("LevelOne");
    }

    public void LoadMainMenu()
    {
        audioPlayer.PlayMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        audioPlayer.PlayMenuMusic();
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void LoadHighScores()
    {
        audioPlayer.PlayMenuMusic();
        SceneManager.LoadScene("HighScores");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
