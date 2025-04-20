using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;
using System;

public class InventoryManager : MonoBehaviour
{
    #region 单例模式
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();//这里是在静态方法中，因此不能直接用this或者gameobject来获取组件
            }
            return _instance;
        }
    }
    #endregion



    #region 解析json
    
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        ParseItemJson();
    }
    //解析物品信息
    void ParseItemJson()
    {
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsjson = itemText.text;//物品信息的json格式
        JsonData jsondata = JsonMapper.ToObject(itemsjson);
        Item itemtemp = null;
        for (int i = 0; i < jsondata.Count; i++)//用物品类型来区分
        {
            int id = (int)jsondata[i]["Id"];
            string name = jsondata[i]["Name"].ToString();
            ItemType itemType = (ItemType)((int)jsondata[i]["ItemType"]);
            Quility quility = (Quility)((int)jsondata[i]["Quility"]);
            int price = (int)jsondata[i]["Price"];
            string description = jsondata[i]["Description"].ToString();
            string prefabPath = jsondata[i]["PrefabPath"].ToString();
            switch (itemType)
            {
                case ItemType.prop:
                    int health = (int)jsondata[i]["Health"];
                    int attackForce = (int)jsondata[i]["AttackForce"];
                    int defenseForce = (int)jsondata[i]["DefenseForce"];
                    itemtemp = new Prop(id,name,itemType,quility,price,description,prefabPath,health,attackForce,defenseForce);
                    break;
                case ItemType.weapon:
                    WeaponType weapontype = (WeaponType)((int)jsondata[i]["WeaponType"]);
                    int damage = (int)jsondata[i]["Damage"];
                    itemtemp = new Weapon(id,name,itemType,quility,price,description,prefabPath,weapontype,damage);
                    break;
                default:
                    break;
            }
            itemList.Add(itemtemp);
        }
        Prop test = (Prop)itemList[2];
        Debug.Log(test.Description);
    }
    #endregion

    #region 获取物品信息
    /// <summary>
    /// 通过 ID 获取物品
    /// </summary>
    /// <param name="id">物品的唯一 ID</param>
    /// <returns>返回匹配的物品对象，如果未找到返回 null</returns>
    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item != null && item.ID == id)
            {
                return item;
            }
        }
        Debug.LogWarning("未找到 ID 为 " + id + " 的物品！");
        return null;
    }
    #endregion
}
