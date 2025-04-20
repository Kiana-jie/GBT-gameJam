using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [Header("Íæ¼Ò1")]
    public Image _hpAmount_player1;
    public Image _expAmount_player1;
    public TextMeshProUGUI _money_player1;
    public TextMeshProUGUI _level_player1;
    public PlayerStatus _status_player1;
    //public TextMeshProUGUI _grade_player1;
    [Header("Íæ¼Ò2")]
    public Image _hpAmount_player2;
    public Image _expAmount_player2;
    public TextMeshProUGUI _money_player2;
    public TextMeshProUGUI _level_player2;
    public PlayerStatus _status_player2;
    //public TextMeshProUGUI _grade_player2;
    public static PlayerInfo Instance;
    private void Awake()
    {
        Instance = this;
        _status_player1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
        _status_player2 = GameObject.Find("Player2").GetComponent <PlayerStatus>();
    }

    public void HPUpdate()
    {
        _hpAmount_player1.fillAmount = (float)_status_player1.health / _status_player1.maxHealth;
        _hpAmount_player2.fillAmount = (float)_status_player2.health / _status_player2.maxHealth;
    }

    public void ExpUpdate()
    {
        _expAmount_player1.fillAmount = (float)_status_player1.currentExp / _status_player1.neededExp;
        _expAmount_player2.fillAmount = (float)_status_player2.currentExp / _status_player2.neededExp;


    }

    public void MoneyUpdate()
    {
        _money_player1.text = _status_player1.money.ToString();
        _money_player2.text = _status_player2.money.ToString();
    }

    public void LevelUpdate()
    {
        _level_player1.text = "Level: " + _status_player1.currentLevel;
        _level_player2.text = "Level: " + _status_player2.currentLevel;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
