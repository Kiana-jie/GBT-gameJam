using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public GameObject itemPrefab;
    private TMPro.TextMeshProUGUI price;
    //private Image slotImage;

    private void Awake()
    {
        price = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

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

        Debug.Log("!");
        {
            itemPrefab = item.itemPrefab;
            GameObject itemGameObject = Instantiate(itemPrefab);
            itemGameObject.transform.SetParent(transform);
            itemGameObject.transform.localPosition = Vector3.zero;
            //itemGameObject.GetComponent<ItemUI>().SetItem(item);
            //更新当前slotUI

            //image
            Image image_slot = gameObject.GetComponent<Image>();
            SpriteRenderer sprite_item = itemPrefab.GetComponent<SpriteRenderer>();
            image_slot.sprite = sprite_item.sprite;
            image_slot.color = sprite_item.color;
            
            
            //name

            //money
            if(price != null)
            {
                price.text = item.Price.ToString();
            }


            Debug.Log(item.Name);
        }
    }

    public int GetItemID() { return transform.GetChild(0).GetComponent<ItemUI>().Item.ID; }
    public int GetShopItemID() { return transform.GetChild(2).GetComponent<ItemUI>().itemID; }






    
}
