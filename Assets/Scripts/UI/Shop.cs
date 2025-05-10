using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Inventory
{
    public PlayerStatus status;
    public Slot selectedSlot;
    public BackPack pack;
    public GameObject player1;
    public GameObject player2;

   
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
                bool success = BuyItem(item.ID);  // 调用购买物品的逻辑
                if(success)
                {
                    selectedSlot.gameObject.SetActive(false);
                    AttributeUI.Instance.AttributeUIUpdate();
                }
                    
            }
            else
            {
                Debug.LogWarning("没有选中的物品或物品不可购买");
            }
        }
    }


   

    


    //商店item抽取,显示时调用
    public void ShopUpdate()
    {
        Debug.Log("刷新商店物品");
        {
            foreach(var slot in slotList)
            {
                slot.gameObject.SetActive(true);
                

                //清除之前的child物体
                foreach (Transform child in slot.transform)
                {
                    if (child.name != "price" && child.name != "Image")
                    {

                        GameObject.Destroy(child.gameObject);
                    }
                }

                slot.StoreItem(InventoryManager.Instance.itemList[Random.Range(0, InventoryManager.Instance.itemList.Count)]);
                

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
            //status.money -= item.Price;  // 扣除金币
            if(item.Type == ItemType.weapon)
            {
                // 判断是否为远程武器或近战武器
                if (IsRangedWeapon(item)) // 远程武器
                {
                    // 获取 Player2 的 Shop 或 BackPack 组件来存储
                    var player2Shop = player2.GetComponent<Shop>();
                    if (player2Shop != null && player2Shop.pack != null)
                    {
                        //判断是否有空背包格
                        if (player2Shop.pack.StoreItem(itemID))
                        {
                            status.money -= item.Price;
                        }
                        else return false;
                    }
                }
                else // 近战武器
                {
                    // 获取 Player1 的 Shop 或 BackPack 组件来存储
                    var player1Shop = player1.GetComponent<Shop>();
                    if (player1Shop != null && player1Shop.pack != null)
                    {
                        if (player1Shop.pack.StoreItem(itemID))
                        {
                            status.money -= item.Price;
                        }
                        else return false;
                    }
                }
            }
            else if(item.Type == ItemType.prop)
            {
                status.money -= item.Price;
                Prop prop = (Prop)item;
                status.maxHealth += prop.Health;
                status.attackForce += prop.AttackForce;
                status.defenceForce += prop.DefenseForce;
            }
            //AudioManager.Instance.Play("buy", gameObject);
            Debug.Log($"成功购买 {item.Name}，花费 {item.Price} 金币");
            //更新UI
            PlayerInfo.Instance.MoneyUpdate();
            ShopMoneyUI.instance.ShopMoneyUIUpdate();
            return true;
        }
        else
        {
            //AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("金币不足，无法购买该物品");
            return false;
        }
    }

    private bool IsRangedWeapon(Item item)
    {
        Weapon weapon = (Weapon)item;
        return weapon.type == WeaponType.ranged;
    }

    private void AssignWeaponToPlayer(Item item, GameObject player)
    {
        
    }
}
