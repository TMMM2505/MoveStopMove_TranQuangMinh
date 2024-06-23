using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Joystick js;
    [SerializeField] private float speed;
    private void Awake()
    {
        base.Awake();

    }
    private void Update()
    {
        base.Update();
        if(alive)
        {
            Move();
            CheckAttack();
        }
    }
    private void Move()
    {
        float x, z;
        x = js.Horizontal;
        z = js.Vertical;
        if(!isAttack)
        {
            if(Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
            {
                rb.velocity = new Vector3(x, 0, z).normalized * speed * Time.deltaTime;
                trans.forward = rb.velocity;
                ChangeAnim("run");
            }
            else if(!canA)
            {
                ChangeAnim("idle");
            }
        }
    }
    private void CheckAttack()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
        {
            canA = false;
        }
        else
        {
            canA = true;
            if (attackRange.GetListCharacter().Count > 0)
            {
                posEnemy = FindClosetE();
                Attack(posEnemy);
            }
        }
    }
}
