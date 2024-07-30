using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Weapon : GameUnit
{
    private Character owner;
    private Vector3 directionBmr;
    private Rigidbody rb;
    private Vector3 startP;
    private bool hit, isBmr = false;

    private void Start()
    {
        startP = transform.position;
    }
    private void Update()
    {
        if (gameObject.CompareTag(Constants.tagSpinW) || gameObject.CompareTag(Constants.tagStraightW))
        {
            if (gameObject.CompareTag(Constants.tagSpinW))
            {
                TF.Rotate(Vector3.back, 800f * Time.deltaTime, Space.Self);
            }
            if (Vector3.Distance(startP, TF.position) >= (owner.GetR() + 3f) && !hit && !isBmr)
            {
                if(this.poolType != PoolType.Boomerang)
                {
                    OnDespawn();
                }
                else
                {
                    Throwed(1200f, -directionBmr);
                    isBmr = true;
                }
            }

            if(Vector3.Distance(TF.position, startP) < 0.3f && isBmr)
            {
                OnDespawn();
            }
        }
    }
/*    public void SetId(int id)
    {
        this.id = id;
    }*/
    public void SetOwner(Character owner)
    {
        if(gameObject)
        {
            this.owner = owner;
        }
    }
    public void Throwed(float force, Vector3 direction)
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(force * direction); 

        if(this.poolType == PoolType.Boomerang)
        {
            directionBmr = direction;
        }
    }
    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.CompareTag(Constants.tagCharacter) && item.GetComponent<Character>() != owner)
        {
            hit = true;
            owner.HitChar?.Invoke(item.GetComponent<Character>());
            OnDespawn();
        }
    }

}
