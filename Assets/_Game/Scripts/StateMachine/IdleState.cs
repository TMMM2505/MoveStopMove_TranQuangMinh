using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.ChangeAnim("IsIdle");
    }
    public void OnExecute(Enemy enemy)
    {
    }
    public void OnExit(Enemy enemy)
    {

    }
}
