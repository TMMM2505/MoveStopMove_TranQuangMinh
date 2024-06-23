using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WeaponData : ScriptableObject
{
    public List<Weapon> weapons;
    public List<GameObject> weaponsAnim;
    public Weapon GetWeapon(WeaponEnum w)
    {
        return weapons[(int)w];
    }
    public GameObject GetWeaponAnim(WeaponEnum w)
    {
        return weaponsAnim[(int)w];
    }
}
public enum WeaponEnum
{
    Axe0 = 0,
    Axe1 = 1,
    Boomarang = 2,
    Candy0 = 3,
    Candy1 = 4,
    Candy2 = 5,
    Candy4 = 6,
    Hammer = 7,
    Uzi = 8,
    Z = 9,
    Arrow = 10,
    Knife = 11,
}
