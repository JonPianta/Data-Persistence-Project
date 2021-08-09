using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;

    [SerializeField] TextMeshProUGUI nameInputField;
    public string nameInput;
    public int highScore = 0;
    public string highScoreName = "Name";
    public KeyValuePair<string, int> highScoreNameScore;
    public List<KeyValuePair<string, int>> scoreList = new List<KeyValuePair<string, int>>();
         
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadScore();
    }

    public void UpdateName()
    {
        nameInput = nameInputField.text;
    }

    [System.Serializable]
    class Savedata
    {
        public int score;
        public string name;
        public KeyValuePair<string, int> nameScore;
        public List<KeyValuePair<string, int>> highScoreList = new List<KeyValuePair<string, int>>();
    }

    public void SaveScore(int rankingScore, string playerName)
    {
        Savedata data = new Savedata();

        //Set Savedata data equal to the highest score achieved and the player's name
        data.score = rankingScore;
        data.name = playerName;
        data.nameScore = new KeyValuePair<string, int>(data.name, data.score);
        data.highScoreList = scoreList;

        //Update high score list if the score achieved is higher than one of the top ten all-time scores
        if(data.highScoreList.Count != 10)
        {
            for (int i = 0; i < data.highScoreList.Count; i++)
            {
                if (data.highScoreList[i].Value < data.score)
                {
                    data.highScoreList.Insert(i, data.nameScore);

                    string json = JsonUtility.ToJson(data);

                    File.WriteAllText(Application.persistentDataPath + "/BScoresList.json", json);

                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < data.highScoreList.Count; i++)
            {
                if(data.highScoreList[i].Value < data.score)
                {
                    data.highScoreList.Insert(i, data.nameScore);
                    data.highScoreList.RemoveAt(10);

                    string json = JsonUtility.ToJson(data);

                    File.WriteAllText(Application.persistentDataPath + "/BScoresList.json", json);

                    return;
                }
            }
        }
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/BScoresList.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Savedata data = JsonUtility.FromJson<Savedata>(json);

            scoreList = data.highScoreList;
            highScore = data.highScoreList[0].Value;
            highScoreName = data.highScoreList[0].Key;
            highScoreNameScore = data.highScoreList[0];
        }
    }
}
