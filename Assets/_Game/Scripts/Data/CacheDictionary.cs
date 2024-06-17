using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CacheDictionary : MonoBehaviour
{
    public static CacheDictionary instance;
    public Dictionary<Collider, Weapon> listWeapon = new Dictionary<Collider, Weapon>();

    private void Start()
    {
        instance = this;
    }

    public Weapon HitWeapon(Collider item)
    {
        if (!listWeapon.ContainsKey(item))
        {
            listWeapon.Add(item, item.GetComponent<Weapon>());
        }

        return listWeapon[item];
    }
}
