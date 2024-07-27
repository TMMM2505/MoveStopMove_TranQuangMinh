using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonOnOff : MonoBehaviour
{
    public GameObject On;
    public GameObject posOn;
    public GameObject Off;
    public GameObject posOff;
    public IconOnOff IconBtn;

    private bool checkOn = true;
    private void Awake()
    {
        IconBtn.GetComponent<Button>().onClick.AddListener(OnClick);
    }

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

        IconBtn.OnClick(posOff.transform.position);
        IconBtn.GetComponent<Image>().color = Color.green;
    }
    public void SetOff()
    {
        On.gameObject.SetActive(false);
        Off.gameObject.SetActive(true);

        IconBtn.OnClick(posOn.transform.position);
        IconBtn.GetComponent<Image>().color = Color.red;
    }
    public bool CheckOn() => checkOn;
}
