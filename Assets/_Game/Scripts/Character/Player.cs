using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;

public class Player : Character
{
    private bool canMove = false;
    private void Awake()
    {
        base.Awake();
        Joystick.direction = Vector3.zero;
        //fullSetItem = SimplePool.Spawn<FullSetItem>(PoolType.FullSetDeadPool, tfFullSetContainer);
        speed = 1100;
    }
    private void Update()
    {
        FindClosetE();
        if (alive)
        {
            Move();
            if (CheckAttack())
            {
                if (attackRange.GetListCharacter().Count > 0)
                {
                    Attack(FindClosetE());
                }
            }
        }
        else
        {
            ResetJoyStick();
        }
    }
    private void Move()
    {
        //if(!isAttack)
        if(canMove)
        {
            if (Mathf.Abs(Joystick.direction.x) > 0.1f || Mathf.Abs(Joystick.direction.z) > 0.1f)
            {
                rb.velocity = Joystick.direction.normalized * speed * Time.deltaTime;
                TF.forward = rb.velocity;
                ChangeAnim("IsRun");
            }
            else if (!canA)
            {
                ChangeAnim("IsIdle");
            }
        }
    }
    public override void SetSpeed(float time)
    {
        speed += (time + speed) / 100;
    }
    public void SetEquippedSkin()
    {
        ChangeSkin(UserData.Ins.GetIdEquippedSkin(), UserData.Ins.GetSkinType());
    }
    public void ResetJoyStick()
    {
        Joystick.direction = Vector3.zero;
    }
    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
