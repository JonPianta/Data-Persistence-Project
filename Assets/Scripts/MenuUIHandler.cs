using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] Text bestScoreText;

    void Start()
    {
        DisplayHighScore();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BestScoresList()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    //Update and recall highest score and the name of the player that earned it
    private void DisplayHighScore()
    {
        bestScoreText.text = "High Score : " + ScoreTracker.instance.highScoreName + " : " + ScoreTracker.instance.highScore;
    }
}
