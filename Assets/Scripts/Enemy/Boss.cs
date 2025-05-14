using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy_1
{
    /*
     * 每隔3s索一次敌
     * 攻击方式：近战+远程弹幕
     * 血量UI
     * 被击败游戏胜利
     * 
     */
    public float searchTimer;


    private void Update()
    {
        searchTimer += Time.deltaTime;
        if (searchTimer >= 3f)
        {
            SearchTarget();
            searchTimer = 0;
        }
    }

    

}
