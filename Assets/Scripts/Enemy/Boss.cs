using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy_1
{
    /*
     * ÿ��3s��һ�ε�
     * ������ʽ����ս+Զ�̵�Ļ
     * Ѫ��UI
     * ��������Ϸʤ��
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
