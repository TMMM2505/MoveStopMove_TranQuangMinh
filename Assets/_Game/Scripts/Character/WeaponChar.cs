using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChar : GameUnit
{
    public void SetParent(Transform hand, Transform parent)
    {
        transform.forward = parent.forward;
        transform.SetParent(hand);
    }
}
