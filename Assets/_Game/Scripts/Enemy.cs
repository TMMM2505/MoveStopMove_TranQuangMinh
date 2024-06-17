using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    private Vector3 goal;
    private NavMeshAgent agent;
    [SerializeField] private State curState;
    private void Start()
    {
        base.Start();
        trans = this.transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrolState());

    }
    private void Update()
    {
        curState.OnExecute(this);
        CheckAtGoal();
    }
    public void OnInit()
    {

    }
    public void FindRandomTarget(float radius)
    {
        Vector3 randomDirection = Random.insideUnitCircle * radius;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        goal = hit.position;
        goal.y = 1f;
        agent.destination = goal;
    }
    public void CheckAtGoal()
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
            agent.destination = trans.position;
            ChangeState(new AttackState());
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
}
