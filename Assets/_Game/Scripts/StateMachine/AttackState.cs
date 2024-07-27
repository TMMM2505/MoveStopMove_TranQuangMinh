using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.Attack(enemy.FindClosetE());
        enemy.IsAttack();
    }
    public void OnExecute(Enemy enemy)
    {
        if (enemy.CheckAttack())
        {
            enemy.RandomNextAction();
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Enemy enemy)
    {

    }
}
