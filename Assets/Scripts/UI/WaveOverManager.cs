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
    public Shop shop1;
    public Shop shop2;

    private void Awake()
    {
        instance = this;
    }

    
    public void ShowWaveOverPanel()
    {
        // ��ͣ��Ϸ
        Time.timeScale = 0f;

        // ��ʾ�̵�ͱ���
        shopPanel.SetActive(true);
        PausePanel.SetActive(true);
        AttributeUI.Instance.AttributeUIUpdate();
        ShopMoneyUI.instance.ShopMoneyUIUpdate();
        shop1.ShopUpdate();
        shop2.ShopUpdate();

        // ����ս����� UI
        gamePanel.SetActive(false);
        playerPanel.SetActive(false);
        exitButton.SetActive(false);
        continueButton.SetActive(false);

        // �ڵ�һ�ֽ���ʱ��ʼ�����װ��
        if(GameManager.Instance.currentWave == 1)
        InitializePlayerItems();
    }

    //���
    public void OnNextButtonClicked()
    {

        //��������������
        var player1 = GameObject.Find("Player1");
        var weaponMgr1 = player1.GetComponent<WeaponManager>();
        if (weaponMgr1 != null)
        {
            weaponMgr1.LoadWeaponsFromSlots();
        }

        var player2 = GameObject.Find("Player2");
        var weaponMgr2 = player2.GetComponent<WeaponManager>();
        if (weaponMgr2 != null)
        {
            weaponMgr2.LoadWeaponsFromSlots();
        }

        shopPanel.SetActive(false);
        PausePanel.SetActive(false);

        gamePanel.SetActive(true);
        playerPanel.SetActive(true);
        exitButton.SetActive(true);
        continueButton.SetActive(true);
        Time.timeScale = 1f;

        GameManager.Instance.AudioPlay();
        
    }

    void InitializePlayerItems()
    {
        if (shop1 != null && shop1.pack != null)
        {
            // ����ұ����д洢��ʼ��Ʒ
            shop1.pack.StoreItem(2);
        }

        if (shop2 != null && shop2.pack != null)
        {
            // ����ұ����д洢��ʼ��Ʒ
            shop2.pack.StoreItem(1);
        }
    }
}
