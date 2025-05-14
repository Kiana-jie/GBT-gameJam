using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinManager : MonoBehaviour
{
    public static GameWinManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    public void Show()
    {

    }
}
