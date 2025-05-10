using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    public GameObject GameOverMenu;
    private void Awake()
    {
        Instance = this;
        GameOverMenu.SetActive(false);
    }
    
    

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
        GameOverMenu.SetActive(false );
        Time.timeScale = 1f;
    }

    public void Show()
    {
        Time.timeScale = 0;
        GameOverMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
