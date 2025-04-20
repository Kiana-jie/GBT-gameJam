using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using LitJson;
using System;

public class InventoryManager : MonoBehaviour
{
    #region ����ģʽ
    private static InventoryManager _instance;
    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();//�������ھ�̬�����У���˲���ֱ����this����gameobject����ȡ���
            }
            return _instance;
        }
    }
    #endregion



    #region ����json
    
    public List<Item> itemList = new List<Item>();

    private void Start()
    {
        ParseItemJson();
    }
    //������Ʒ��Ϣ
    void ParseItemJson()
    {
        TextAsset itemText = Resources.Load<TextAsset>("Items");
        string itemsjson = itemText.text;//��Ʒ��Ϣ��json��ʽ
        JsonData jsondata = JsonMapper.ToObject(itemsjson);
        Item itemtemp = null;
        for (int i = 0; i < jsondata.Count; i++)//����Ʒ����������
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

    #region ��ȡ��Ʒ��Ϣ
    /// <summary>
    /// ͨ�� ID ��ȡ��Ʒ
    /// </summary>
    /// <param name="id">��Ʒ��Ψһ ID</param>
    /// <returns>����ƥ�����Ʒ�������δ�ҵ����� null</returns>
    public Item GetItemByID(int id)
    {
        foreach (Item item in itemList)
        {
            if (item != null && item.ID == id)
            {
                return item;
            }
        }
        Debug.LogWarning("δ�ҵ� ID Ϊ " + id + " ����Ʒ��");
        return null;
    }
    #endregion
}
