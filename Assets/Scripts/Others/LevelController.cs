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

    //�������
    public void WaveComplete()
    {
        if(!waveCompleted)
        {
            waveCompleted = true;
            //������й���
            EnemyManager.instance.DestroyAllEnemies();
            

            //���̵���� //��ת���̵���棬�ڼ���������UI�����̵�ͱ���panel�������ť������Ϸ����Ϸʱ��ع���������ʼ��һ��

            //����
            GameManager.Instance.currentWave += 1;
            Debug.Log("�������,����� " + GameManager.Instance.currentWave + "��");
            GameInfo.Instance.WaveCountUpdate();
            WaveTimeInitiate();
            //��������Ѫ��
            PlayerStatus status1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
            PlayerStatus status2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();
            status1.health = status1.maxHealth;
            status2.health = status2.maxHealth;
            PlayerInfo.Instance.HPUpdate();


        }
    }

    
}
