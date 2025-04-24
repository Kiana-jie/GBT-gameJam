using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    [Range(0, 3)]
    public int energy = 0;
    
    public void AddEnergy()
    {
        if(energy < 3)
        energy++;
        //同步UI，image里加载
        GameInfo.Instance.UpdateEnergyUI();
    }

    //返回跳跃等级：
    //1：1级跳
    //2：2级跳
    public int ConsumeEnergy()
    {
        if(energy < 3&& energy > 0)
        {
            energy--;
            GameInfo.Instance.UpdateEnergyUI();
            return 1;
        }
        else 
        {
            energy = 0;
            GameInfo.Instance.UpdateEnergyUI();
            return 2;
        }
    }

    
}
