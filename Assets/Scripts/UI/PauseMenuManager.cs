using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    private bool isPaused = false;

    void Update()
    {
        // ��� ESC ��
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
        pauseMenuUI.SetActive(false); // ������ͣ�˵�
        Time.timeScale = 1f; // �ָ���Ϸʱ��
        isPaused = false;
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true); // ��ʾ��ͣ�˵�
        Time.timeScale = 0f; // ��ͣ��Ϸʱ��
        AttributeUI.Instance.AttributeUIUpdate();
        isPaused = true;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
}