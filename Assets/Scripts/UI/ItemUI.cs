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

    public void SetItem(Item item, int amount = 1)//�ò���item����itemUI�е�item
    {
        this.Item = item;
        if (this.Item == null)
        {
            Debug.LogError("����� Item Ϊ null");
        }
        //����UI
        //itemImage.sprite = Resources.Load<Sprite>(it);//����item�е�sprite����Resources�ļ��е�ͼƬ����ֵ����ǰslot�е�item
    }
    

}
