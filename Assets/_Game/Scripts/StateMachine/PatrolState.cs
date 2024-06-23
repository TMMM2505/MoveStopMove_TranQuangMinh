using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.FindRandomTarget();
        enemy.ChangeAnim("run");
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.CheckOnMove();
    }
    public void OnExit(Enemy enemy)
    {

    }
}
