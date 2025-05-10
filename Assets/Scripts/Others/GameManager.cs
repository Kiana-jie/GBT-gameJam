using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("基本信息")]
    public int currentWave = 1;
    [Header("时空坍缩参数")]
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


        // 保存原始材质
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
        int attackedWorldId = UnityEngine.Random.Range(1, 3);//世界1和世界2
        yield return StartCoroutine(WaringWorldAttack(attackedWorldId));
        
        //获取id下所有的player，造成玩家当前生命值的伤害
        List<PlayerStatus> players = GetPlayer(attackedWorldId);
        Debug.Log(players);
        if (players.Count != 0)
        {
            //直接死亡
            if (players.Count == 1)
            {
                players[0].health = 0;
                PlayerInfo.Instance.HPUpdate();
            }
            //每人减一半血
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


    //预警信息 更换材质，world1 Front圈内，back圈外；world2 front圈外，back圈内
    public IEnumerator WaringWorldAttack(int id)
    {
        Debug.Log("!");
        // 创建警告材质
        Material warningMat = new Material(Shader.Find("Sprites/Default"));
        warningMat.color = warningColor;

        // 根据攻击的世界设置不同部分的材质
        if (id == 1) // 世界1被攻击
        {
            back_world1.material = warningMat;
            front_world1.material = warningMat;
        }
        else // 世界2被攻击
        {
            front_world2.material = warningMat;
            back_world2.material = warningMat;
        }

        // 闪烁效果
        float timer = 0f;
        while (timer < warningTime)
        {
            float color_offset = Mathf.PingPong(timer * flashSpeed, 1f);
            warningMat.color = new Color(warningColor.r + color_offset, warningColor.g + color_offset, warningColor.b + color_offset);

            timer += Time.deltaTime;
            yield return null;
        }

        // 恢复原始材质
        ResetWorldMaterials();
    }

    private void ResetWorldMaterials()
    {
        front_world1.material = originalFrontMat1;
        back_world1.material = originalBackMat1;
        front_world2.material = originalFrontMat2;
        back_world2.material = originalBackMat2;
    }

    //检查是否可以发动世界攻击
    /*
     * 当前波次剩余时间大于10s
     * 玩家具备跳跃条件（有能量)
     */
    public bool CheckAttackCondition()
    {
        return LevelController.Instance.waveTimer >= 10f && EnergyManager.Instance.energy > 0; 
    }


}
