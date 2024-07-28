using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ButtonOnOff : MonoBehaviour
{
    public GameObject On;
    public GameObject posOn;
    public GameObject Off;
    public GameObject posOff;

    private bool checkOn;
    public void OnClick()
    {
        if (checkOn)
        {
            checkOn = false;
            SetOff();
        }
        else
        {
            checkOn = true;
            SetOn();
        }
    }

    public void SetUp()
    {
        if(checkOn)
        {
            SetOn();
        }
        else
        {
            SetOff();
        }
    }
    public void SetOn()
    {
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);

        OnClick(posOff.transform.position);
        GetComponent<Image>().color = Color.green;
    }
    public void SetOff()
    {
        On.gameObject.SetActive(false);
        Off.gameObject.SetActive(true);

        OnClick(posOn.transform.position);
        GetComponent<Image>().color = Color.red;
    }
    public bool CheckOn() => checkOn;
    public void OnClick(Vector3 pos)
    {
        //transform.DOMove(pos, 1f, true).SetEase(Ease.Linear);
        transform.position = pos;
    }
}
