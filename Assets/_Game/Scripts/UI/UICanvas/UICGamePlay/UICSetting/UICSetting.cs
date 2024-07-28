using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICSetting : UICanvas
{
    public ButtonOnOff BtnSound;
    public ButtonOnOff BtnVibration;
    public Button BtnHome;
    public Button BtnContinue;
    private void Awake()
    {
        BtnSound.GetComponent<Button>().onClick.AddListener(SoundOnclick);
        BtnVibration.GetComponent<Button>().onClick.AddListener(VibrationOnclick);
        BtnHome.onClick.AddListener(HomeOnClick);
        BtnContinue.onClick.AddListener(ContinueOnClick);
    }
    private void Start()
    {
        BtnSound.SetUp();
        BtnVibration.SetUp();
    }
    private void HomeOnClick()
    {
        LevelManager.Ins.Replay();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close(0);
    }

    private void ContinueOnClick()
    {
        Close(0);
        Time.timeScale = 1f;
        UIManager.Ins.OpenUI<UICGamePlay>();
    }

    private void VibrationOnclick()
    {
        BtnVibration.OnClick();
        UserData.Ins.SetVibe(BtnVibration.CheckOn());
        Debug.Log(UserData.Ins.GetCheckOnSound() + " " + UserData.Ins.GetCheckOnVibe());
    }

    private void SoundOnclick()
    {
        BtnSound.OnClick();
        UserData.Ins.SetSound(BtnSound.CheckOn());
        Debug.Log(UserData.Ins.GetCheckOnSound() + " " + UserData.Ins.GetCheckOnVibe());

    }
    public void SetUpFunction()
    {
        Debug.Log("Setting Start: " + UserData.Ins.GetCheckOnSound() + UserData.Ins.GetCheckOnVibe());
        if (UserData.Ins.GetCheckOnSound())
        {
            BtnSound.SetOn();
            Debug.Log("sound on");
        }
        else
        {
            BtnSound.SetOff();
            Debug.Log("sound off");
        }
        if (UserData.Ins.GetCheckOnVibe())
        {
            BtnVibration.SetOn();
            Debug.Log("vibe on");
        }
        else
        {
            BtnVibration.SetOff();
            Debug.Log("vibe off");
        }
    }
}
