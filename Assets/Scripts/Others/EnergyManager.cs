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
        //ͬ��UI��image�����
    }

    //������Ծ�ȼ���
    //1��1����
    //2��2����
    public int ConsumeEnergy()
    {
        if(energy < 3&& energy > 0)
        {
            energy--;
            return 1;
        }
        else 
        {
            energy = 0;
            return 2;
        }
    }

    
}
