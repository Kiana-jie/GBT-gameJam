using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Inventory
{
    public PlayerStatus status;
    public Slot selectedSlot;
    public BackPack pack;
    

    // 购买按钮点击事件
    public void OnBuyButtonClicked()    
    {
        //设置slelectedSlot为当前点击的slot
        if (selectedSlot != null)
        {
            int itemId = selectedSlot.GetShopItemID();  // 使用 GetItemID 获取物品 ID
            Item item = InventoryManager.Instance.GetItemByID(itemId);  // 获取物品

            if (item != null)
            {
                BuyItem(item.ID);  // 调用购买物品的逻辑
            }
            else
            {
                Debug.LogWarning("没有选中的物品或物品不可购买");
            }
        }
    }


   

    // 商店界面显示与隐藏
    public void ShowShopPanel()
    {
        if (canvasGroup != null)
        {
            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // 切换透明度
            canvasGroup.interactable = !isVisible;  // 切换交互性
            
        }
    }

    //商店item抽取,显示时调用
    public void ShopUpdate()
    {
        if(canvasGroup.interactable)
        {
            foreach(var slot in slotList)
            {
                slot.StoreItem(InventoryManager.Instance.itemList[Random.Range(0, InventoryManager.Instance.itemList.Count - 1)]);
            }
        }
    }

    public bool BuyItem(int itemID)
    {
        Item item = InventoryManager.Instance.GetItemByID(itemID); // 从物品管理器获取物品
        if (item == null) // 如果物品不存在或不是可以买的物品
        {
            Debug.LogWarning("无法购买该物品");
            return false;
        }
        // 检查玩家是否有足够的金币
        if (status.money >= item.Price)
        {
            // 购买物品
            status.money -= item.Price;  // 扣除金币
            if(item.Type == ItemType.weapon)
            {
                pack.StoreItem(itemID);
            }
            else if(item.Type == ItemType.prop)
            {
                Prop prop = (Prop)item;
                status.maxHealth += prop.Health;
                status.attackForce += prop.AttackForce;
                status.defenceForce += prop.DefenseForce;
            }
            //AudioManager.Instance.Play("buy", gameObject);
            Debug.Log($"成功购买 {item.Name}，花费 {item.Price} 金币");
            //更新UI
            PlayerInfo.Instance.MoneyUpdate();
            return true;
        }
        else
        {
            //AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("金币不足，无法购买该物品");
            return false;
        }
    }
}
