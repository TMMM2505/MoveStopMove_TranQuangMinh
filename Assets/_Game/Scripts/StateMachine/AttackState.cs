using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public void OnStart(Enemy enemy)
    {
        enemy.Attack(enemy.FindClosetE());
    }
    public void OnExecute(Enemy enemy)
    {
        enemy.CheckOnMove();                        
    }
    public void OnExit(Enemy enemy)
    {

    }
}
