using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public LayerMask ObstacleLayer;
    public float speed = 20;

    private Vector3 direction;
    private Obstacles lastObstacles;
    void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
    public void OnMainMenu()
    {
        offset.y = 1f;
        offset.z = -10f;
        transform.rotation = Quaternion.Euler(10, 0, 0);
    }public void OnShopSkin()
    {
        offset.y = 2f;
        offset.z = -13f;
        transform.rotation = Quaternion.Euler(20, 0, 0);
    }
    public void OnGamePlay()
    {
        offset.y = 15f;
        offset.z = -25f;
        transform.rotation = Quaternion.Euler(30, 0, 0);
    }
    public void Change()
    {
        offset.y += 1.5f;
        offset.z -= 1.5f;
    }
    
}
