using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.ChangeAnim("idle");
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.CheckOnMove();
    }
    public void OnExit(Enemy enemy)
    {

    }
}
