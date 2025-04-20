using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : Item
{
    
    public int Health;
    public int AttackForce;
    public int DefenseForce;

    public Prop(int id, string name,ItemType itemType, Quility quility, int price, string description, string prefabPath,int health,int attackForce,int defenseForce):base(id,name,itemType,quility,price,description,prefabPath)
    {
        Health = health;
        AttackForce = attackForce;
        DefenseForce = defenseForce;
    }

    public Prop() { }
}
