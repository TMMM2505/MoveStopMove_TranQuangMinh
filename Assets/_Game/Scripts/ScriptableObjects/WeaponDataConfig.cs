using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "WeaponDataConfig", menuName = "Scriptable Objects/Weapon Data Config", order = 1)]


[Serializable]
public class WeaponDataConfig : ScriptableObject
{
    public List<WeaponData> ListDataWeapon = new();
    public Weapon GetWeapon(int id)
    {
        return ListDataWeapon[id].prefabWeapon;
    }
    public WeaponChar GetWeaponChar(int id)
    {
        return ListDataWeapon[id].prefabWeaponChar;
    }
}


[Serializable]
public class WeaponData
{
    public int ID;
    public string Name;
    public string Lock;
    public string Description;
    public Sprite Icon;
    public float Price;
    public WeaponEnum WeaponType;
    public EBuffWeapon BuffType;
    public float Value;
    public Weapon prefabWeapon;
    public WeaponChar prefabWeaponChar;
}
public enum WeaponEnum
{
    none = 1,
    Candy0 = 2,
    Knife = 3,
    Candy1 = 4,
    Boomarang = 5,
    Candy4 = 6,
    Axe0 = 7,
    Candy2 = 8,
    Axe1 = 9,
    Z = 10,
    Arrow = 11,
    Uzi = 12,
}
public enum EBuffWeapon
{
    Range = 1,
    AttackSpeed = 2,
}
