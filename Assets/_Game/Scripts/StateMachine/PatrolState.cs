using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class PatrolState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.FindRandomTarget(10f);
        enemy.ChangeAnim("run");
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.CheckAtGoal();
    }
    public void OnExit(Enemy enemy)
    {

    }
}
