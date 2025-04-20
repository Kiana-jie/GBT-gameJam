using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // 拖入你的暂停菜单 Panel
    private bool isPaused = false;

    void Update()
    {
        // 检测 ESC 键
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
        pauseMenuUI.SetActive(false); // 隐藏暂停菜单
        Time.timeScale = 1f; // 恢复游戏时间
        isPaused = false;
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // 显示暂停菜单
        Time.timeScale = 0f; // 暂停游戏时间
        isPaused = true;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}