using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slotList = new List<Slot>();
    public CanvasGroup canvasGroup;
    // Start is called before the first frame update
    public virtual void Start()
    {
        slotList.AddRange(GetComponentsInChildren<Slot>());//������bug
        canvasGroup = GetComponent<CanvasGroup>();
        //��ʼ������
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }

    // Update is called once per frame

    public bool StoreItem(int id)
    {
        Item item = InventoryManager.Instance.GetItemByID(id);
        if (item == null)
        {
            Debug.LogWarning("Ҫ�������Ʒid������");
            return false;
        }
        
        Slot slot = FindEmptySlot();
        if (slot == null)
        {
            Debug.LogWarning("û�пյ���Ʒ��");
            return false;
        }
        else
        {
            slot.StoreItem(item);//����Ʒ�洢������Ʒ������
        }
        
        //Debug.Log(item.Name);
        return true;
    }

    private Slot FindEmptySlot()
    {
        foreach (Slot slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return null;
    }
}


