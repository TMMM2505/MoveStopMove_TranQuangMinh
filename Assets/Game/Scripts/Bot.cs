using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    [SerializeField] private Animator anim;


    private NavMeshAgent agent;
    private Vector3[] Des = new Vector3[21];
    private float[] Min = new float[21];
    private Vector3 goal;

    void Start()
    {
        int k = 0;
        float x, z;
        System.Random _x = new System.Random();
        System.Random _z = new System.Random();
        while (k <= 20)
        {
            x = _x.Next(0, 20);
            z = _z.Next(0, 20);
            Des[k] = new Vector3(x, 0, z);
            k++;
        }
        agent = GetComponent<NavMeshAgent>();
        calculate();
        FindDes();
        agent.destination = goal;
        anim.SetTrigger("run");
    }

    private void Update()
    {
        if (CheckReset())
        {
            calculate();
        }
        if (goal.x == transform.position.x && goal.z == transform.position.z)
        {
            FindDes();
            agent.destination = goal;
        }
    }

    private void calculate()
    {
        for (int i = 0; i <= 20; i++)
            Min[i] = Vector3.Distance(Des[i], transform.position);
    }

    private bool CheckReset()
    {
        int test = 0;
        for(int i = 0; i <= 20; i++)
        {
            if (Min[i] != 99999) test++;
        }

        return test == 0;
    }

    private void FindDes()
    {
        for(int i = 0; i <= 20; i++)
            if (Min[i] == Min.Min())
            {
                Debug.Log("findMin");
                goal = Des[i];
                Min[i] = 99999999f;
                return;
            }
    }
}
