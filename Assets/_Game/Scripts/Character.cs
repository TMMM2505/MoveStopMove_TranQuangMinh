using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Character : GameUnit
{
    public static Character instance;

    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    public AttackRange attackRange;

    protected Vector3 posEnemy;
    protected Weapon prefabWeapon;
    protected Transform trans;


    protected int amountBullet = 1;
    protected int hp = 1;
    protected float forceThrow;
    protected string currentAnim;
    protected bool inAttackRange, canA;

    public Action<Character> OnDeadRemove;
    protected void Start()
    {
        instance = this;
        trans = transform;
        
    }

    protected void OnTriggerEnter(Collider item)
    {
        if(item.gameObject.CompareTag(Constants.tagSpinW) || item.gameObject.CompareTag(Constants.tagStraightW))
        {
            Debug.Log("hitted");
            Hitted();
        }
    }

    IEnumerator DelayAttack(float time, Vector3 direction, float corner)
    {
        yield return new WaitForSeconds(time);
        if(canA)
        {
            prefabWeapon = SimplePool.Spawn<Weapon>(PoolType.Weapon, trans.position + trans.forward * 1.5f, Quaternion.Euler(-90, 90, corner));
            if(prefabWeapon != null)
            {
            }
            prefabWeapon.Throwed(500f, direction.normalized);
        }
    }
    IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        amountBullet = 1;
    }
    IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
    protected void Hitted()
    {
        ChangeAnim("fall");
        gameObject.SetActive(false);
        OnDeadRemove?.Invoke(this);
        Debug.Log(this);
    }
    public void Attack(Vector3 posEnemy)
    {
        Vector3 direction = posEnemy - trans.position;
        if(amountBullet == 1)
        {
            trans.forward = direction;
            amountBullet = 0;
            direction.y = 0;

            Vector2 directionA = new Vector2(trans.position.x, trans.position.z);
            Vector2 directionB = new Vector2(posEnemy.x, posEnemy.z);

            Vector2 kq = directionB - directionA;
            float angle = Mathf.Atan2(kq.y, kq.x);  

            if (angle > Mathf.PI)
            {
                angle -= Mathf.PI;
            }
            else if (angle < -Mathf.PI)
            {
                angle += Mathf.PI;
            }

            float angleBetweenDeg = angle * Mathf.Rad2Deg;

            StartCoroutine(DelayAttack(0.7f, direction, angleBetweenDeg * -1));
            StartCoroutine(CoolDown(2f));
            ChangeAnim("idle");
        }
    }
    public Vector3 FindClosetE()
    {
        float[] distance = new float[100];
        float min = 0;
        Vector3 location = Vector3.zero;
        for(int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            distance[i] = Vector3.Distance(trans.position, attackRange.GetListCharacter()[i].transform.position);
        }
        min = distance[0];
        for (int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            if (distance[i] < min)
            {
                min = distance[i];
            }
        }
        for (int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            if (distance[i] == min)
            {
                location = attackRange.GetListCharacter()[i].transform.position;
            }
        }

        return location;
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    public void CharacterEnterAttackRange(Character character)
    {
        if (attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Add(character);  
    }

    public void CharacterExitAttackRange(Character character)
    {
        if (!attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Remove(character);
    }

    public void RemoveCharacterFromList(Character character)
    {
        if (!attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Remove(character);   
    }
}

