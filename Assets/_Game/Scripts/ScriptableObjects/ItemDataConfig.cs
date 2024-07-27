using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDataConfig", menuName = "Scriptable Objects/Item Data Config", order = 1)]
public class ItemDataConfig : ScriptableObject
{
    //public List<ItemData> DataConfigs = new();
    public List<ListHat> ListHats = new();
    public List<ListPant> ListPants = new();
    public List<ListShield> ListShields = new();
    public List<ListFullSet> ListFullSet = new();
/*    public ItemData GetItemDataByID(int id)
    {
        for (int i = 0; i < DataConfigs.Count; i++)
        {
            ItemData data = DataConfigs[i];
            if (data.ID != id) continue;
            return data;
        }
        return null;
    }*/
}

[Serializable]
public class ItemData
{
    public int ID;
    public string Name;
    public string Description;
    public Sprite Icon;
    public float Price;
    public EItemType ItemType;
    public EBuffType BuffType;
    public float Value;
}
[Serializable]
public class ListHat
{
    public ItemData data;
    public GameObject hat;
}
[Serializable]
public class ListPant
{
    public ItemData data;
    public Material pant;
}
[Serializable]
public class ListShield
{
    public ItemData data;
    public GameObject shield;
}
[Serializable]
public class ListFullSet
{
    public ItemData data;
    public Material setFull;
    public GameObject hat;
    public Material pant;
    public GameObject wing;
    public GameObject tail;
    public GameObject leftHandWeapon;
}
public enum EItemType
{
    None = 1,
    Hat = 2,
    Pant = 3,
    Weapon = 4,
    Shield = 5,
    FullSet = 6,
}

public enum EBuffType
{
    None = 0, 
    GoldEarnPerKill = 1,
    GoldEarnEndMatch = 2,
    SpeedBonus = 3,
    SpeefBonusAfterKill = 4,
    LargerRange = 5,
}