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
    
    //点击装备显示操作面板(1，如果有两件相同的，可合成；2.可卖出（五折卖出）
    
    

    public void ShowBackpackPanel()
    {
        if (canvasGroup != null)
        {

            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // 切换透明度
            canvasGroup.interactable = !isVisible;  // 切换交互性
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
