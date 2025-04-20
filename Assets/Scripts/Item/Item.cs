using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    prop,   // 道具
    weapon, //武器，区分近战远程和工程武器
}

public enum Quility
{
    common,
    rare
}
[System.Serializable]
public class Item
{
    public int ID {  get; set; }
    public string Name { get; set; }
    public ItemType Type { get; set; }
    public Quility quility { get; set; }
    public int Price {  get; set; }
    public string Description { get; set; }
    public string PrefabPath { get; set; }
    // Start is called before the first frame update
    [NonSerialized]
    public GameObject itemPrefab;
    
    public Item(int iD, string name,ItemType itemType, Quility quility ,int price, string description,string path)
    {
        ID = iD;
        Name = name;
        Type = itemType;
        this.quility = quility;
        Price = price;
        Description = description;
        PrefabPath = path;

        LoadPrefab();
    }
    public Item() { }

    public void LoadPrefab()
    {
        if (!string.IsNullOrEmpty(PrefabPath))
        {
            itemPrefab = Resources.Load<GameObject>(PrefabPath);
            if (itemPrefab == null)
            {
                Debug.LogWarning($"未能加载 prefab：{PrefabPath}");
            }
        }
    }

}
