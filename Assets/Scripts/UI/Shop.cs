using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Inventory
{
    public PlayerStatus status;
    public Slot selectedSlot;
    public BackPack pack;
    

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
                BuyItem(item.ID);  // ���ù�����Ʒ���߼�
            }
            else
            {
                Debug.LogWarning("û��ѡ�е���Ʒ����Ʒ���ɹ���");
            }
        }
    }


   

    // �̵������ʾ������
    public void ShowShopPanel()
    {
        if (canvasGroup != null)
        {
            bool isVisible = canvasGroup.alpha == 1;
            canvasGroup.alpha = isVisible ? 0 : 1;  // �л�͸����
            canvasGroup.interactable = !isVisible;  // �л�������
            
        }
    }

    //�̵�item��ȡ,��ʾʱ����
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
            status.money -= item.Price;  // �۳����
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
            Debug.Log($"�ɹ����� {item.Name}������ {item.Price} ���");
            //����UI
            PlayerInfo.Instance.MoneyUpdate();
            return true;
        }
        else
        {
            //AudioManager.Instance.Play("error", gameObject);
            Debug.LogWarning("��Ҳ��㣬�޷��������Ʒ");
            return false;
        }
    }
}
