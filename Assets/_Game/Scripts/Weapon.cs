using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Weapon : GameUnit
{
    private string id;
    private Rigidbody rb;
    private Vector3 startP;
    private bool hit;
    private Character owner;

    private void Start()
    {
        startP = transform.position;
    }
    private void Update()
    {
        if (gameObject.CompareTag(Constants.tagSpinW) || gameObject.CompareTag(Constants.tagSpinW))
        {
            if (gameObject.CompareTag(Constants.tagSpinW))
            {
                transform.Rotate(Vector3.back, 500f * Time.deltaTime, Space.Self);
            }
            if (Vector3.Distance(startP, transform.position) >= (owner.getR() + 3f) && !hit)
            {
                OnDespawn();
            }
        }
    }
    public void SetOwner(Character owner)
    {
        this.owner = owner;
    }
    public void Throwed(float force, Vector3 direction)
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(force * direction); 
    }
    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.CompareTag(Constants.tagCharacter) && item.GetComponent<Character>() != owner)
        {
            hit = true;
            owner.HitChar?.Invoke();
            OnDespawn();
        }
    }
}
