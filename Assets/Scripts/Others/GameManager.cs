using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("������Ϣ")]
    public int currentWave = 1;
    [Header("ʱ��̮������")]
    public float worldAttackTime = 10f;
    private float worldAttackTimer;
    public float warningTime = 3f;
    public Color warningColor = Color.red;
    public float flashSpeed = 2f;

    public GameObject World1;
    public GameObject World2;


    private SpriteRenderer front_world1;
    private SpriteRenderer back_world1;
    private SpriteRenderer front_world2;
    private SpriteRenderer back_world2;

    private Material originalFrontMat1;
    private Material originalBackMat1;
    private Material originalFrontMat2;
    private Material originalBackMat2;

    
    private Shader warningShader;
    private void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
        front_world1 = World1.transform.Find("World1 Front").GetComponent<SpriteRenderer>();
        front_world2 = World2.transform.Find("World2 Front").GetComponent<SpriteRenderer>();
        back_world1 = World1.transform.Find("World1 Back").GetComponent<SpriteRenderer>();
        back_world2 = World2.transform.Find("World2 Back").GetComponent<SpriteRenderer>();


        // ����ԭʼ����
        originalFrontMat1 = front_world1.material;
        originalFrontMat2 = front_world2.material;
        originalBackMat1 = back_world1.material;
        originalBackMat2 = back_world2.material;

        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        worldAttackTimer += Time.deltaTime;
        if (worldAttackTimer >= worldAttackTime)
        {
            worldAttackTimer = 0;
            bool condition = CheckAttackCondition();
            if (condition)
            {
                StartCoroutine(WorldAttack());
            }
        }

    }


    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverManager.Instance.Show();
    }

    /**/
    public IEnumerator WorldAttack()
    {
        int attackedWorldId = UnityEngine.Random.Range(1, 3);//����1������2
        yield return StartCoroutine(WaringWorldAttack(attackedWorldId));
        
        //��ȡid�����е�player�������ҵ�ǰ����ֵ���˺�
        List<PlayerStatus> players = GetPlayer(attackedWorldId);
        Debug.Log(players);
        if (players.Count != 0)
        {
            //ֱ������
            if (players.Count == 1)
            {
                players[0].health = 0;
                PlayerInfo.Instance.HPUpdate();
            }
            //ÿ�˼�һ��Ѫ
            else if (players.Count == 2)
            {
                players[0].health /= 2;
                players[1].health /= 2;
                PlayerInfo.Instance.HPUpdate();
            }
        }
        yield return null;
    }

    private List<PlayerStatus> GetPlayer(int attackedWorldId)
    {
        List<PlayerStatus> playerStatusList = new List<PlayerStatus>();
        PlayerStatus player1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
        if (player1.currentWorld == attackedWorldId)
        {
            playerStatusList.Add(player1);
        }
        PlayerStatus player2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();
        if (player2.currentWorld == attackedWorldId)
        {
            playerStatusList.Add(player2);
        }
        return playerStatusList;
    }


    //Ԥ����Ϣ �������ʣ�world1 FrontȦ�ڣ�backȦ�⣻world2 frontȦ�⣬backȦ��
    public IEnumerator WaringWorldAttack(int id)
    {
        Debug.Log("!");
        // �����������
        Material warningMat = new Material(Shader.Find("Sprites/Default"));
        warningMat.color = warningColor;

        // ���ݹ������������ò�ͬ���ֵĲ���
        if (id == 1) // ����1������
        {
            back_world1.material = warningMat;
            front_world1.material = warningMat;
        }
        else // ����2������
        {
            front_world2.material = warningMat;
            back_world2.material = warningMat;
        }

        // ��˸Ч��
        float timer = 0f;
        while (timer < warningTime)
        {
            float color_offset = Mathf.PingPong(timer * flashSpeed, 1f);
            warningMat.color = new Color(warningColor.r + color_offset, warningColor.g + color_offset, warningColor.b + color_offset);

            timer += Time.deltaTime;
            yield return null;
        }

        // �ָ�ԭʼ����
        ResetWorldMaterials();
    }

    private void ResetWorldMaterials()
    {
        front_world1.material = originalFrontMat1;
        back_world1.material = originalBackMat1;
        front_world2.material = originalFrontMat2;
        back_world2.material = originalBackMat2;
    }

    //����Ƿ���Է������繥��
    /*
     * ��ǰ����ʣ��ʱ�����10s
     * ��Ҿ߱���Ծ������������)
     */
    public bool CheckAttackCondition()
    {
        return LevelController.Instance.waveTimer >= 10f && EnergyManager.Instance.energy > 0; 
    }


}
