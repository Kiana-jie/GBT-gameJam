using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public float waveTimer;
    private bool waveCompleted = false;
    public Shop shop1;
    public Shop shop2;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {  
        WaveTimeInitiate();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;

            if(waveTimer < 0)
            {
                waveTimer = 0;
                WaveComplete();
                
            }
        }

        GameInfo.Instance.CountDownUpdate(waveTimer);
    }



    public void WaveTimeInitiate()
    {
        waveTimer = 15 + 5 * GameManager.Instance.currentWave;
        waveCompleted = false;
    }

    //波次完成
    public void WaveComplete()
    {
        if(!waveCompleted)
        {
            waveCompleted = true;
            //清除所有怪物
            EnemyManager.instance.DestroyAllEnemies();
            

            //打开商店面板 //跳转到商店界面，期间隐藏其他UI，打开商店和背包panel，点击按钮继续游戏后游戏时间回归正常，开始下一波

            //更新
            GameManager.Instance.currentWave += 1;
            Debug.Log("波次完成,进入第 " + GameManager.Instance.currentWave + "波");
            GameInfo.Instance.WaveCountUpdate();
            WaveTimeInitiate();
            //更新人物血量
            PlayerStatus status1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
            PlayerStatus status2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();
            status1.health = status1.maxHealth;
            status2.health = status2.maxHealth;
            PlayerInfo.Instance.HPUpdate();


        }
    }

    
}
