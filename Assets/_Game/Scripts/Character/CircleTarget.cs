using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CircleTarget : GameUnit
{
    public void SetPosition(Character enemy)
    {
        TF.SetParent(enemy.TF);
        TF.position = enemy.TF.position - new Vector3(0, 0.5f, 0);
    }
    public void RemoveFromTarget()
    {
        OnDespawn();
    }
}
