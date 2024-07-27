using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject[] startPoint;
    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    public GameObject[] GetStartPoint() => startPoint;
}
