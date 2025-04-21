using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    
    //private Image slotImage;

   

    public void OnBuyButtonClicked()
    {
        Shop shop = GetComponentInParent<Shop>();
        if (shop != null)
        {
            shop.selectedSlot = this;
        }
        shop.OnBuyButtonClicked();
    }
    public void StoreItem(Item item)
    {
       
        
        {
            itemPrefab = item.itemPrefab;
            GameObject itemGameObject = Instantiate(itemPrefab);
            itemGameObject.transform.SetParent(transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            //itemGameObject.GetComponent<ItemUI>().SetItem(item);
            Debug.Log(item.Name);
        }
    }

    public int GetItemID() { return transform.GetChild(0).GetComponent<ItemUI>().Item.ID; }
    public int GetShopItemID() { return transform.GetChild(0).GetComponent<ItemUI>().itemID; }






    
}
