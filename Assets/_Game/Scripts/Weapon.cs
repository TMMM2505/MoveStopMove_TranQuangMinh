using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Weapon : GameUnit
{
    private Rigidbody rb;
    private Vector3 startP;
    private bool hit;

    private void Start()
    {
        startP = transform.position;
    }
    private void Update()
    {
        if(gameObject.CompareTag(Constants.tagSpinW))
        {
            transform.Rotate(Vector3.back, 500f * Time.deltaTime, Space.Self);
        }
        if (Vector3.Distance(startP, transform.position) >= 8.5f && !hit)
        {
            Destroy(gameObject);
        }
    }
    public void Throwed(float force, Vector3 direction)
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(force * direction); 
    }
    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.CompareTag(Constants.tagEnemy) || item.gameObject.CompareTag(Constants.tagPlayer))
        {
            hit = true;
            Destroy(gameObject);
        }
    }
}
