using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private State curState;

    private Vector3 goal;
    private NavMeshAgent agent;
    private bool attack, randomAction = true;
    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        base.Update();
        if(alive)
        {
            curState.OnExecute(this);
            CheckOnMove();
        }
        else
        {
            agent.destination = trans.position;
            curState.OnExit(this);
        }
    }

    public void EnemyOnInit()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState());
    }

    public void FindRandomTarget()
    {
        int r = Random.Range(20, 30);
        Vector3 randomDirection = Random.insideUnitSphere * r;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, r, NavMesh.AllAreas);
        goal = hit.position;
        goal.y = 1f;
        agent.destination = goal;
    }
    public void CheckOnMove()
    {
        if(!CheckAttack())
        {
            if (Vector3.Distance(trans.position, goal) < 0.5f)
            {
                ChangeState(new PatrolState());
            }
        }
        else
        {
            if(randomAction)
            {
                randomAction = false;
                Invoke(nameof(DelayRandomAction), 0.7f);
                Invoke(nameof(ResetRandomAction), 0.7f);
            }
        }
    }
    public void ChangeState(State newState)
    {
        if (curState != null)
        {
            curState.OnExit(this);
        }
        curState = newState;
        if(curState != null)
        {
            curState.OnStart(this);
        }
    }
    public bool CheckAttack()
    {
        if(attackRange.GetListCharacter().Count > 0)
        {
            canA = true;
            
        }
        else
        {
            canA = false;
        }
        return canA;
    }

    void DelayRandomAction()
    {
        int attack = Random.Range(0, 2);
        if(alive)
        {
            if (attack == 1)
            {
                agent.destination = trans.position;
                if(CheckAttack())
                {
                    ChangeState(new AttackState());
                }
                else
                {
                    ChangeState(new PatrolState());
                }
            }
            else
            {
                ChangeState(new PatrolState());
            }
        }
    }
    void ResetRandomAction()
    {
        randomAction = true;
    }
}
