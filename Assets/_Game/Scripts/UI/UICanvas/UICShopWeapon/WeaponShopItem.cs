using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopItem : UICanvas
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Lock;
    public TextMeshProUGUI price;
    public Image Icon;

    public Button BtnBuy;
    public Button BtnOwned;
    public Button BtnEquipped;

    public bool Owned, Equipped;
    private void Start()
    {

    }

    public void SetUpWeaponItem(WeaponData item)
    {
        Name.text = item.Name;
        Description.text = item.Description;
        Lock.text = item.Lock;
        price.text = item.Price.ToString();
        Icon.sprite = item.Icon;
  }
}
