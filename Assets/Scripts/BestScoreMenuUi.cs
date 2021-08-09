using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BestScoreMenuUi : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bestScoresText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
