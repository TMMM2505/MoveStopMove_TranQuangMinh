using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICMainMenu : UICanvas
{
    public Button BtnPlay;
    public Button BtnShopWeapon;
    public Button BtnShopSkin;
    public Button BtnChoseMap;

    public ItemSetting BtnSound;
    public ItemSetting BtnVibe;
    public TextMeshProUGUI coin;
    private void Awake()
    {
        BtnPlay.onClick.AddListener(OnClickPlay);
        BtnShopWeapon.onClick.AddListener(OnClickShopWeapon);
        BtnShopSkin.onClick.AddListener(OnClickShopSkin);
        BtnChoseMap.onClick.AddListener(OnClickChoseMap);

        BtnSound.GetComponent<Button>().onClick.AddListener(OnClickSound);
        BtnVibe.GetComponent<Button>().onClick.AddListener(OnClickVibe);
    }

    private void OnClickVibe()
    {
        BtnVibe.OnClick();
        UserData.Ins.SetVibe(BtnVibe.CheckOn());
        Debug.Log(UserData.Ins.GetCheckOnSound() + " " + UserData.Ins.GetCheckOnVibe());
    }

    private void OnClickSound()
    {
        BtnSound.OnClick();
        UserData.Ins.SetSound(BtnSound.CheckOn());
        Debug.Log(UserData.Ins.GetCheckOnSound() + " " + UserData.Ins.GetCheckOnVibe());
    }

    private void OnClickChoseMap()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.ChoseMap);
    }

    private void Start()
    {
        UIManager.Ins.SaveLastCanvas(this);
        LevelManager.Ins.ChangeWeaponForPlayer(UserData.Ins.GetIdEquippedWeapon());
        LevelManager.Ins.DeactiveEnemy();
    }
    private void Update()
    {
        coin.text = UserData.Ins.GetCoin().ToString();
    }
    private void OnClickPlay()
    {
        LevelManager.Ins.OnStartGame();
        GameManager.Ins.ChangeState(GameState.Gameplay);
        LevelManager.Ins.ActivePlayer();
        Close(0);
    }

    private void OnClickShopSkin()
    {
        GameManager.Ins.ChangeState(GameState.ShopSkin);
        Close(0);
    }

    private void OnClickShopWeapon()
    {
        GameManager.Ins.ChangeState(GameState.ShopWeapon);
        LevelManager.Ins.DeactivePlayer();
        Close(0);
    }
    public void SetUpFunction()
    {
        Debug.Log(UserData.Ins.GetCheckOnSound() + " " + UserData.Ins.GetCheckOnVibe());
        if (UserData.Ins.GetCheckOnSound())
        {
            BtnSound.TurnOn();
        }
        else
        {
            BtnSound.TurnOff();
        }
        if (UserData.Ins.GetCheckOnVibe())
        {
            BtnVibe.TurnOn();
        }
        else
        {
            BtnVibe.TurnOff();
        }
    }

}
