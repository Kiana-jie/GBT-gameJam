using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    melee,
    ranged
}
public class Weapon : Item
{
     

    public WeaponType type;
    public int Damage { get; set; }
    
    public Weapon (int id,string name, ItemType type, Quility quility,int price,string description,string prefabPath,WeaponType weapontype,int damage): base(id,name,type,quility,price,description,prefabPath)
    {
        this.type = weapontype;
        Damage = damage;
    }

    public Weapon() { }
    
}
