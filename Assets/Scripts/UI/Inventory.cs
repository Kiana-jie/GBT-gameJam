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
        slotList.AddRange(GetComponentsInChildren<Slot>());//可能有bug
        canvasGroup = GetComponent<CanvasGroup>();
        //初始不启用
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
            Debug.LogWarning("要储存的物品id不存在");
            return false;
        }
        
        Slot slot = FindEmptySlot();
        if (slot == null)
        {
            Debug.LogWarning("没有空的物品槽");
            return false;
        }
        else
        {
            slot.StoreItem(item);//把物品存储到空物品槽里面
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


