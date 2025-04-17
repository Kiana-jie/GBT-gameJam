using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment
{
    public EquipType Type {  get;  set; }
    public int Grade {  get; set; }
    public int Damage { get; set; }
    
    public Weapon (int id,string name,int price,string description,string sprite,EquipType type,int grade,int damage): base(id,name,price,description,sprite)
    {
        Type = type;
        Grade = grade;
        Damage = damage;
    }

    
}
