using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class IdleState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.ChangeAnim("idle");
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.ChangeState(new IdleState());
    }
    public void OnExit(Enemy enemy)
    {

    }
}
