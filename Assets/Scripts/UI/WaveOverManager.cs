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
        // 暂停游戏
        Time.timeScale = 0f;

        // 显示商店和背包
        shopPanel.SetActive(true);
        PausePanel.SetActive(true);
        AttributeUI.Instance.AttributeUIUpdate();
        ShopMoneyUI.instance.ShopMoneyUIUpdate();
        shop1.ShopUpdate();
        shop2.ShopUpdate();

        // 隐藏战斗相关 UI
        gamePanel.SetActive(false);
        playerPanel.SetActive(false);
        exitButton.SetActive(false);
        continueButton.SetActive(false);

        // 在第一轮结束时初始化玩家装备
        if(GameManager.Instance.currentWave == 1)
        InitializePlayerItems();
    }

    //点击
    public void OnNextButtonClicked()
    {

        //载入武器到人物
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
            // 在玩家背包中存储初始物品
            shop1.pack.StoreItem(2);
        }

        if (shop2 != null && shop2.pack != null)
        {
            // 在玩家背包中存储初始物品
            shop2.pack.StoreItem(1);
        }
    }
}
