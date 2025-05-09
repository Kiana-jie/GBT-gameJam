using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    private bool isPaused = false;

    void Update()
    {
        // ºÏ≤‚ ESC º¸
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // “˛≤ÿ‘›Õ£≤Àµ•
        Time.timeScale = 1f; // ª÷∏¥”Œœ∑ ±º‰
        isPaused = false;
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // œ‘ æ‘›Õ£≤Àµ•
        Time.timeScale = 0f; // ‘›Õ£”Œœ∑ ±º‰
        AttributeUI.Instance.AttributeUIUpdate();
        isPaused = true;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}