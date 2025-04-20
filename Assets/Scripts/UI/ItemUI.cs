using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Item Item { get; private set; }

    private Image itemImage;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public void SetItem(Item item, int amount = 1)//用参数item设置itemUI中的item
    {
        this.Item = item;
        if (this.Item == null)
        {
            Debug.LogError("传入的 Item 为 null");
        }
        //更新UI
        //itemImage.sprite = Resources.Load<Sprite>(it);//根据item中的sprite加载Resources文件中的图片并赋值给当前slot中的item
    }
    

}
