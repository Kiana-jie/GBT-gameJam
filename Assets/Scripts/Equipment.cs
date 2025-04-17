using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public enum EquipType
    {
        props,   // 道具
        weapon, //武器，区分近战远程和工程武器
    }

    public int ID {  get; set; }
    public string Name { get; set; }
    public int Price {  get; set; }
    public string Description { get; set; }
    public string Sprite { get; set; }
    // Start is called before the first frame update
    public Equipment(int iD, string name, int price, string description,string sprite)
    {
        ID = iD;
        Name = name;
        Price = price;
        Description = description;
        Sprite = sprite;
    }
    public Equipment() { }

}
