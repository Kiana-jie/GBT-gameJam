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

   
    // ����ť����¼�
    public void OnBuyButtonClicked()    
    {
        //����slelectedSlotΪ��ǰ�����slot
        if (selectedSlot != null)
        {
            int itemId = selectedSlot.GetShopItemID();  // ʹ�� GetItemID ��ȡ��Ʒ ID
            Item item = InventoryManager.Instance.GetItemByID(itemId);  // ��ȡ��Ʒ

            if (item != null)
            {
                bool success = BuyItem(item.ID);  // ���ù�����Ʒ���߼�
                if(success)
                {
                    selectedSlot.gameObject.SetActive(false);
                    AttributeUI.Instance.AttributeUIUpdate();
                }
                    
            }
            else
            {
                Debug.LogWarning("û��ѡ�е���Ʒ����Ʒ���ɹ���");
            }
        }
    }


   

    


    //�̵�item��ȡ,��ʾʱ����
    public void ShopUpdate()
    {
        Debug.Log("ˢ���̵���Ʒ");
        {
            foreach(var slot in slotList)
            {
                slot.gameObject.SetActive(true);
                

                //���֮ǰ��child����
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
        Item item = InventoryManager.Instance.GetItemByID(itemID); // ����Ʒ��������ȡ��Ʒ
        if (item == null) // �����Ʒ�����ڻ��ǿ��������Ʒ
        {
            Debug.LogWarning("�޷��������Ʒ");
            return false;
        }
        // �������Ƿ����㹻�Ľ��
        if (status.money >= item.Price)
        {
            // ������Ʒ
            //status.money -= item.Price;  // �۳����
            if(item.Type == ItemType.weapon)
            {
                // �ж��Ƿ�ΪԶ���������ս����
                if (IsRangedWeapon(item)) // Զ������
                {
                    // ��ȡ Player2 �� Shop �� BackPack ������洢
                    var player2Shop = player2.GetComponent<Shop>();
                    if (player2Shop != null && player2Shop.pack != null)
                    {
                        //�ж��Ƿ��пձ�����
                        if (player2Shop.pack.StoreItem(itemID))
                        {
                            status.money -= item.Price;
                        }
                        else return false;
                    }
                }
                else // ��ս����
                {
                    // ��ȡ Player1 �� Shop �� BackPack ������洢
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
            Debug.Log($"�ɹ����� {item.Name}������ {item.Price} ���");
            //����UI
            PlayerInfo.Instance.MoneyUpdate();
            ShopMoneyUI.instance.ShopMoneyUIUpdate();
            return true;
        }
        else
        {
            //AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("��Ҳ��㣬�޷��������Ʒ");
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
