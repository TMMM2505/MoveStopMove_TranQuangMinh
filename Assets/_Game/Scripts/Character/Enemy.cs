using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : Character
{
    private State curState;

    private Vector3 goal;
    private NavMeshAgent agent;
    private bool attack, checkRandomAction = true;
    private int getRandomAction = -1;
    private void Awake()
    {
        base.Awake();
    }
    private void Update()
    {
        if(alive)
        {
            curState.OnExecute(this);
        }
        else
        {
            curState.OnExit(this);
            agent.destination = TF.position;
        }
    }
    public bool CheckAttack()
    {
        if (attackRange.GetListCharacter().Count > 0)
        {
            canA = true;
        }
        else
        {
            canA = false;
        }
        return canA;
    }
    public void EnemyOnInit()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new IdleState());
        int r1 = Random.Range(0, 4);
        int maxR2 = 2;
        int r2;
        if(r1 == 0)
        {
            maxR2 = GameManager.Ins.ItemDataConfig.ListHats.Count;
            r2 = Random.Range(0, maxR2);
            ChangeSkin(r2, EItemType.Hat);
        }
        else if(r1 == 1) 
        {
            maxR2 = GameManager.Ins.ItemDataConfig.ListPants.Count;
            r2 = Random.Range(0, maxR2);
            ChangeSkin(r2, EItemType.Pant);
        }
        else if (r1 == 2)
        {
            maxR2 = GameManager.Ins.ItemDataConfig.ListShields.Count;
            r2 = Random.Range(0, maxR2);
            ChangeSkin(r2, EItemType.Shield);
        }
        else if (r1 == 3)
        {
            maxR2 = GameManager.Ins.ItemDataConfig.ListFullSet.Count;
            r2 = Random.Range(0, maxR2);
            ChangeSkin(r2, EItemType.FullSet);
        }
    }
    public void FindRandomTarget()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            int r = Random.Range(20, 30);
            Vector3 randomDirection = Random.insideUnitSphere * r;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, r, NavMesh.AllAreas);
            goal = hit.position;
            goal = randomDirection;
            goal.y = 1f;
            agent.destination = goal;
        }
        else
        {
            agent.destination = LevelManager.Ins.GetPlayer().TF.position;
        }
    }
    public void RandomNextAction()
    {
        if(checkRandomAction)
        {
            StartCoroutine(DelayRandomNextAction());
            if(getRandomAction == 0 && !isAttack)
            {
                ChangeState(new PatrolState());
            }
            else if (getRandomAction == 1)
            {
                ChangeState(new AttackState());
            }
            StartCoroutine(ResetCheckRandomAction());
        }
    }
    IEnumerator DelayRandomNextAction()
    {
        yield return new WaitForSeconds(0.7f);
        getRandomAction = Random.Range(0, 2);
        checkRandomAction = false;
    }
    IEnumerator ResetCheckRandomAction()
    {
        yield return new WaitForSeconds(1f);
        checkRandomAction = true;
    }
    public bool IsAttack()
    {
        agent.destination = TF.position;
        return isAttack;
    }
    public Vector3 GetGoal() => goal;
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
    public void ResetCharacter()
    {
        base.ResetCharacter();
        curState.OnExit(this);
    }
    public void DeActive()
    {
        if(this)
        {
            StopAllCoroutines();
            agent.destination = TF.position;
            ChangeState(new IdleState());
        }
    }
    public override void SetSpeed(float time)
    {
        float tmpSpeed = GetComponent<NavMeshAgent>().speed;
        tmpSpeed += (time + tmpSpeed) / 100;
        GetComponent<NavMeshAgent>().speed = tmpSpeed;
    }
}
