using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BackPack : Inventory
{   
    private void Update()
    {
       
    }

    public void SellEquipment()
    {
        
    }
    
    //���װ����ʾ�������(1�������������ͬ�ģ��ɺϳɣ�2.������������������
    
    

    public void ShowBackpackPanel()
    {
        if (canvasGroup != null)
        {

            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // �л�͸����
            canvasGroup.interactable = !isVisible;  // �л�������
            /*if (!isVisible)
            {
                AudioManager.Instance.Play("invenOpen", gameObject);
            }
            else
            {
                AudioManager.Instance.Play("invenClose", gameObject);

            }*/

        }
    }
}
