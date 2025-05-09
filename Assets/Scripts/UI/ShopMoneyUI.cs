using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ShopMoneyUI : MonoBehaviour
{
    public static ShopMoneyUI instance;
    public TextMeshProUGUI money1;
    public TextMeshProUGUI money2;

    private PlayerStatus status1;
    private PlayerStatus status2;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        status1 = GameObject.Find("Player1").GetComponent<PlayerStatus>();
        status2 = GameObject.Find("Player2").GetComponent<PlayerStatus>();
        
    }
    
    public void ShopMoneyUIUpdate()
    {
        money1.text = status1.money.ToString();
        money2.text = status2.money.ToString();
    }
}
