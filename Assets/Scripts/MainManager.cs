using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    private int brickCount;
    public Rigidbody Rb_Ball;
    public Rigidbody paddle;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    [SerializeField] Ball ball;
    
    public bool m_Started = false;
    public bool addScore = true;
    public int m_Points;
    
    public bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        ScoreTracker.instance.LoadScore();
        bestScoreText.text = "Best Score : " + ScoreTracker.instance.highScoreName + " : " + ScoreTracker.instance.highScore;
        ScoreText.text = $"Score : {ScoreTracker.instance.nameInput} : {m_Points}";

        brickCount = 0;

        NewRound();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Rb_Ball.transform.SetParent(null);
                Rb_Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if(addScore)
            {
                addScore = false; 
                ScoreTracker.instance.SaveScore(m_Points, ScoreTracker.instance.nameInput);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (brickCount == 0)
        {
            NewRound();
        }
    }

    private void NewRound()
    {
        //Reset the ball's position to the paddle, set velocity to 0, and let the ball be fired from the paddle again
        m_Started = false;
        Rb_Ball.velocity = Vector3.zero.normalized;
        Rb_Ball.angularVelocity = Vector3.zero.normalized;
        ball.m_velocity = Vector3.zero;
        ball.transform.position = paddle.transform.position + new Vector3(0, 0.15f, 0);
        ball.transform.SetParent(paddle.transform);
        
        //Code to spawn the bricks for the level. Takes the length of a brick and calculates how many bricks to create per row based on the space allotted for the level.
        //The number of rows is determined by a LineCount variable.
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                ++brickCount;
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        --brickCount;
        ScoreText.text = $"Score : {ScoreTracker.instance.nameInput} : {m_Points}";
        if(m_Points > ScoreTracker.instance.highScore)
        {
            bestScoreText.text = "Best Score : " + ScoreTracker.instance.nameInput + " : " + m_Points;
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
