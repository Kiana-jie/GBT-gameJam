using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject GameFailMenu;
    public GameObject GameWinMenu;
    private void Awake()
    {
        Instance = this;
        GameFailMenu.SetActive(false);
        GameWinMenu.SetActive(false);
    }
    
    

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        GameFailMenu.SetActive(false );
        GameWinMenu.SetActive(false );
        Time.timeScale = 1f;
    }

    public void ShowFailPanel()
    {
        Time.timeScale = 0;
        GameFailMenu.SetActive(true);
    }

    public void ShowWinPanel()
    {
        Time.timeScale = 0;
        GameWinMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
