using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class Character : GameUnit
{
    public static Character instance;

    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CapsuleCollider col;
    [SerializeField] protected GameObject holdWeapon;
    public AttackRange attackRange;

    protected List<Weapon> myWeapon = new List<Weapon>();
    protected Vector3 posEnemy;
    protected Weapon weapon;
    protected Transform trans;

    protected bool alive = true;
    protected int amountBullet = 1;
    protected float r;
    protected float forceThrow, increase = 1.5f;
    protected string currentAnim;
    protected bool inAttackRange, canA, isAttack;

    public Action<Character> OnDeadRemove;
    public Action HitChar;

    protected void Awake()
    {
        trans = transform;
        rb = GetComponent<Rigidbody>();
    }
    protected void Update()
    {
        CheckMove();
    }
    public void OnInit(GameObject weaponChar, Weapon weapon)
    {
        alive = true;
        amountBullet = 1;
        col = GetComponent<CapsuleCollider>();
        r = attackRange.transform.localScale.x / 2;
        HitChar = HitTartget;

        this.weapon = weapon;

        GameObject prefabChar = Instantiate(weaponChar, holdWeapon.transform.position, Quaternion.identity);
        prefabChar.transform.SetParent(holdWeapon.transform);
    }
    protected void CheckMove()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
        {
            canA = false;
        }
        else
        {
            canA = true;
        }
    }
    protected void OnTriggerEnter(Collider item)
    {
        if(item.gameObject.CompareTag(Constants.tagSpinW) || item.gameObject.CompareTag(Constants.tagStraightW))
        {
            if(!myWeapon.Contains(item.GetComponent<Weapon>()))
            {
                Debug.Log("die");
                alive = false;
                Hitted();
            }
            else
            {
                Debug.Log("my W");
            }
        }
    }

    IEnumerator DelayAttack(float time, Vector3 direction, float corner)
    {
        yield return new WaitForSeconds(time);
        if(canA)
        {
            weapon = SimplePool.Spawn<Weapon>(weapon.poolType, trans.position + trans.forward * increase, Quaternion.Euler(-90, 90, corner));
            weapon.SetOwner(this);
            myWeapon.Add(weapon);
            weapon.Throwed(500f, direction.normalized);
            isAttack = false;
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
        OnDespawn();
    }
    protected void Hitted()
    {
        ChangeAnim("fall");
        OnDeadRemove?.Invoke(this);
        rb.useGravity = false;
        col.enabled = false;
        StartCoroutine(Dead(2f));
    }
    protected void HitTartget()
    {
        trans.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        attackRange.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        increase += 0.2f;
        r = attackRange.transform.localScale.x / 2;
    }
    public float getR() => r;
    public void Attack(Vector3 posEnemy)
    {
        if(alive)
        {
            Vector3 direction = posEnemy - trans.position;
            if(amountBullet == 1)
            {
                ChangeAnim("throw");

                isAttack = true;

                direction.y = 0;
                trans.forward = direction;
                amountBullet = 0;

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
            }
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
    public bool IsDead()
    {
        return !alive;
    }
}

