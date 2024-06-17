using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]   
public class AttackState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.ChangeAnim("throw");
        enemy.Attack(enemy.FindClosetE());
    }
    public void OnExecute(Enemy enemy)
    {
        if (enemy.CheckAttack())
        {
            enemy.ChangeState(new AttackState());
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
