using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.FindRandomTarget();
        enemy.ChangeAnim("IsRun");
    }
    public void OnExecute(Enemy enemy)
    {
        if(enemy.CheckAttack())
        {
            enemy.RandomNextAction();
        }
        if (Vector3.Distance(enemy.TF.position, enemy.GetGoal()) < 0.3f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
