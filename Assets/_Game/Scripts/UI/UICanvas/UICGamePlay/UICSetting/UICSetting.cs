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
        Time.timeScale = 1f;
        LevelManager.Ins.Replay();
        LevelManager.Ins.OnInit();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        Close(0);
    }

    private void ContinueOnClick()
    {
        Close(0);
        Time.timeScale = 1f;
        GameManager.Ins.ChangeState(GameState.Gameplay);
    }

    private void VibrationOnclick()
    {
        BtnVibration.OnClick();
        UserData.Ins.SetVibe(BtnVibration.CheckOn());
    }

    private void SoundOnclick()
    {
        BtnSound.OnClick();
        UserData.Ins.SetSound(BtnSound.CheckOn());
    }
    public void SetUpFunction()
    {
        if (UserData.Ins.GetCheckOnSound())
        {
            BtnSound.SetOn();
        }
        else
        {
            BtnSound.SetOff();
        }
        if (UserData.Ins.GetCheckOnVibe())
        {
            BtnVibration.SetOn();
        }
        else
        {
            BtnVibration.SetOff();
        }
    }
}
