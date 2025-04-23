using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance;
    public TextMeshProUGUI _countDown;
    public TextMeshProUGUI _waveCount;
    
    public Image[] energys;
    //public Stack<Image> currentEnergy;
    private void Awake()
    {
        Instance = this;
        
    }

    //���µ���ʱ
    public void CountDownUpdate(float timer)
    {
        _countDown.text = ((int)timer).ToString();
    }

    public void WaveCountUpdate()
    {
        _waveCount.text = "Wave " + GameManager.Instance.currentWave;
    }

    
}
