using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopWeapon : UICanvas
{
    [SerializeField] WeaponShopItem weaponShopItem;
    private int indexWeapon;
    private List<WeaponData> weapons = new();

    public Button BtnLeft;
    public Button BtnRight;
    public Button BtnExit;

    public TextMeshProUGUI txtCoin;

    private void Awake()
    {
        BtnLeft.onClick.AddListener(BtnLeftOnClick);
        BtnRight.onClick.AddListener(BtnRightOnClick);
        BtnExit.onClick.AddListener(BtnExitOnClick);

        weaponShopItem.BtnBuy.onClick.AddListener(BtnBuyOnClick);
        weaponShopItem.BtnOwned.onClick.AddListener(BtnOwnedOnClick);
    }
    private void Start()
    {
        indexWeapon = 0;
        SetUpShopWeapon();
        SetItem();
        txtCoin.text = UserData.Ins.GetCoin().ToString();
        CheckOwned();
    }
    private void BtnOwnedOnClick()
    {
        TurnOnBtnEquipped();
        txtCoin.text = UserData.Ins.GetCoin().ToString();
        UserData.Ins.SetIdEquippedWeapon(weapons[indexWeapon].ID);
        LevelManager.Ins.ChangeWeaponForPlayer(UserData.Ins.GetIdEquippedWeapon());
    }

    private void BtnBuyOnClick()
    {
        if(UserData.Ins.GetCoin() >= weapons[indexWeapon].Price)
        {
            TurnOnBtnOwned();
            UserData.Ins.SetCoin(UserData.Ins.GetCoin() - (int)weapons[indexWeapon].Price);
            UserData.Ins.SaveListWeaponId(weapons[indexWeapon].ID);
        }
        txtCoin.text = UserData.Ins.GetCoin().ToString();
    }

    private void BtnExitOnClick()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void BtnRightOnClick()
    {
        if(indexWeapon < weapons.Count - 1)
        {
            indexWeapon++;
            SetItem();
            CheckOwned();
        }
    }

    private void BtnLeftOnClick()
    {
        if (indexWeapon > 0)
        {
            indexWeapon--;
            SetItem();
            CheckOwned();
        }
    }

    public void SetUpShopWeapon()
    {
        for(int i = 0; i < LevelManager.Ins.weaponData.ListDataWeapon.Count; i++)
        {
            WeaponData w = LevelManager.Ins.weaponData.ListDataWeapon[i];
            weapons.Add(w);
        }
    }
    public void SetItem()
    {
        weaponShopItem.SetUpWeaponItem(weapons[indexWeapon]);
    }
    public void TurnOnBtnOwned()
    {
        weaponShopItem.BtnBuy.gameObject.SetActive(false);
        weaponShopItem.BtnOwned.gameObject.SetActive(true);
        weaponShopItem.BtnEquipped.gameObject.SetActive(false);
    }
    public void TurnOnBtnEquipped()
    {
        weaponShopItem.BtnBuy.gameObject.SetActive(false);
        weaponShopItem.BtnOwned.gameObject.SetActive(false);
        weaponShopItem.BtnEquipped.gameObject.SetActive(true);
    }
    public void TurnOnBtnBuy()
    {
        weaponShopItem.BtnBuy.gameObject.SetActive(true);
        weaponShopItem.BtnOwned.gameObject.SetActive(false);
        weaponShopItem.BtnEquipped.gameObject.SetActive(false);
    }
    public void CheckOwned()
    {
        if (UserData.Ins.ListOwnedWeaponId.Contains(weapons[indexWeapon].ID))
        {
            if (UserData.Ins.GetIdEquippedWeapon() == weapons[indexWeapon].ID)
            {
                TurnOnBtnEquipped();
            }
            else
            {
                TurnOnBtnOwned();
            }
        }
        else
        {
            TurnOnBtnBuy();
        }
    }
}
