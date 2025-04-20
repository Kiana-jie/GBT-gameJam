using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOverManager : MonoBehaviour
{
    public static WaveOverManager instance;
    public GameObject shopPanel;
    public GameObject PausePanel;
    public GameObject exitButton;
    public GameObject continueButton;
    public GameObject playerPanel;
    public GameObject gamePanel;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ShowWaveOverPanel();
    }
    public void ShowWaveOverPanel()
    {
        // ��ͣ��Ϸ
        Time.timeScale = 0f;

        // ��ʾ�̵�ͱ���
        shopPanel.SetActive(true);
        PausePanel.SetActive(true);

        // ����ս����� UI
        gamePanel.SetActive(false);
        playerPanel.SetActive(false);
        exitButton.SetActive(false);
        continueButton.SetActive(false);
        
    }

    //���
    public void OnNextButtonClicked()
    {
        
        shopPanel.SetActive(false);
        PausePanel.SetActive(false);

        gamePanel.SetActive(true);
        playerPanel.SetActive(true);
        exitButton.SetActive(true);
        continueButton.SetActive(true);
        Time.timeScale = 1f;

    }
}
