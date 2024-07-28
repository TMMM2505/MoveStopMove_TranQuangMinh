using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSetting : MonoBehaviour
{
    public Image On, Off;
    private bool checkOn;
    public void OnClick()
    {
        if(checkOn)
        {
            TurnOff();
            checkOn = false;
        }
        else
        {
            TurnOn();
            checkOn = true;
        }
    }
    public void TurnOn()
    {
        On.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
    }
    public void TurnOff()
    {
        On.gameObject.SetActive(false);
        Off.gameObject.SetActive(true);
    }
    public bool CheckOn() => checkOn;
}
