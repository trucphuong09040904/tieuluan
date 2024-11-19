using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOver;

    public Image ImageHP;
    public GameObject scoreUITextGO; //reference to the score text UI game object

    public void UpdateHP(float currentHP, float maxHP)
    {
       ImageHP.fillAmount = currentHP/maxHP;
    }

    public void Over()
    {
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }


    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        scoreUITextGO.GetComponent<GameScore>().Score = 0;
    }

    public void menu()
    {
        SceneManager.LoadScene(1);
    }

    public void quit()
    {
        Application.Quit();
    }
}
