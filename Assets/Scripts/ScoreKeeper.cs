using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{    
    int currentScore;
    string playerName;
    Dictionary<string, int> previousHighScores = new Dictionary<string, int>();

    static ScoreKeeper instance;
    private const int maxHighScores = 10;

    void Awake()
    {
        ManageSingleton();
        LoadPreviousHighScores();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public int GetCurrentScore()
    {
        return instance.currentScore;
    }

    public void ModifyScore(int points)
    {
        currentScore += points;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public Dictionary<string, int> GetPreviousHighScores()
    {
        return previousHighScores;
    }

    public void AddPlayerName(string name)
    {
        playerName = name;
    }    

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SaveHighScore()
    {
        var scoresNames = new List<string>();
        SaveData data = new SaveData();

        //Sort on highest score on top
        var sortedScores = (from entry in previousHighScores orderby entry.Value descending select entry).ToDictionary(x => x.Key, x => x.Value);

        //If player improved score after restart, remove previous score, otherwise don't save the score
        if (sortedScores.ContainsKey(playerName))
        {
            if (sortedScores[playerName] < instance.currentScore)
            {
                sortedScores.Remove(playerName);
            }
            else
            {
                return;
            }
        }

        var position = SearchHallOfFame(sortedScores, currentScore);

        if (position == null && sortedScores.Count < maxHighScores)
        {
            sortedScores.Add(playerName, currentScore);
        }
        else
        {
            //Remove last item, add new item and sort again
            if (sortedScores.Count >= maxHighScores)
            {
                sortedScores.Remove(sortedScores.Keys.Last());
            }
            sortedScores.Add(playerName, currentScore);
            sortedScores = (from entry in sortedScores orderby entry.Value descending select entry).ToDictionary(x => x.Key, x => x.Value);
        }

        //Convert scores to list and save
        foreach (var item in sortedScores)
        {
            scoresNames.Add(item.Key + " - " + item.Value);
        }

        //Save data
        data.HighScores = scoresNames;
        string dataJson = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + @"/savefile.json", dataJson);

        //Update scores
        previousHighScores = sortedScores;
    }

    void LoadPreviousHighScores()
    {
        string path = Application.persistentDataPath + @"/savefile.json";
        if (File.Exists(path))
        {
            string dataJson = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(dataJson);

            foreach (string score in data.HighScores)
            {
                var points = GetScore(score);
                previousHighScores.Add(score.Split(new char[0])[0], points);
            }
        }
    }    

    //Return null when score is not higher then any scores in the list
    int? SearchHallOfFame(Dictionary<string, int> currentScores, int score)
    {
        for (int i = 0; i < currentScores.Count; i++)
        {
            var scoreItem = currentScores.ElementAt(i);

            if (score > scoreItem.Value)
            {
                return i;
            }
        }
        return null;
    }

    int GetScore(string score)
    {
        return Convert.ToInt32(score.Split('-')[1].Substring(1));
    }


    [System.Serializable]
    class SaveData
    {
        public List<string> HighScores;
    }
}
