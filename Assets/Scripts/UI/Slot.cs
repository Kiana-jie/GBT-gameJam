using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    
    //private Image slotImage;

    private void Start()
    {
        Button button = GetComponent<Button>();
        
        if (button != null )
        {
            button.onClick.AddListener(OnBuyButtonClicked);
        }
        //slotImage = GetComponent<Image>();
    }

    public void OnBuyButtonClicked()
    {
        Shop shop = GetComponentInParent<Shop>();
        if (shop != null)
        {
            shop.selectedSlot = this;
        }
    }
    public void StoreItem(Item item)
    {
        if (transform.childCount == 0)
        {
            GameObject itemGameObject = Instantiate(itemPrefab);
            itemGameObject.transform.SetParent(transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            itemGameObject.GetComponent<ItemUI>().SetItem(item);
        }
    }

    public int GetItemID() { return transform.GetChild(0).GetComponent<ItemUI>().Item.ID; }
    public int GetShopItemID() { return transform.GetChild(0).GetComponent<ShopItemUI>().itemID; }





    public void OnSlotClicked()//µã»÷¹ºÂò
    {
        
    }

}
