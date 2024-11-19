using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    int score;
    public int targetScore1 = 1000;
    public int targetScore2 = 2000;

    public string scene1 = "PlayScene";
    public string scene2 = "PlayScene 1";
    public string scene3 = "PlayScene 2";
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreTextUI();
            CheckAndLoadNextScene();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //get the Text UI component of this game Object
        scoreTextUI = GetComponent<Text>();
        UpdateScoreTextUI();
    }

    //function to update the score text UI
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format ("{0:000000}", score);
        scoreTextUI.text = scoreStr;
    }

    void CheckAndLoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == scene1 && score >= targetScore1)
        {
            LoadNextScene(scene2);
        }
        else if (SceneManager.GetActiveScene().name == scene2 && score >= targetScore2)
        {
            LoadNextScene(scene3);
        }
    }
    void LoadNextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
