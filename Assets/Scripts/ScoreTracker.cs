using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ScoreTracker : MonoBehaviour
{

    public static ScoreTracker instance;

    [SerializeField] MainManager mainManager;

    [SerializeField] TextMeshProUGUI nameInputField;
    public string nameInput;
    public int highScore = 0;
    public string highScoreName = "Name";

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
    }

    public void SaveScore(int highScore, string playerName)
    {
        Savedata data = new Savedata();
        //Set Savedata data equal to the highest score achieved and the player's name
        data.score = highScore;
        data.name = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/Hiscores.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/Hiscores.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            Savedata data = JsonUtility.FromJson<Savedata>(json);
            
            highScore = data.score;
            highScoreName = data.name;
        }
    }
}
