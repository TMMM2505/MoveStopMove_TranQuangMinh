using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkin : UICanvas
{
    /*public ButtonTag BtnHat;
    public ButtonTag BtnPant;
    public ButtonTag BtnShield;
    public ButtonTag BtnClothes;

    public Button BtnExit;
    public Button BtnBuy;
    public Button BtnOwned;
    public Button BtnEquipped;

    public TextMeshProUGUI coin;

    public ContentShopSkin Content;

    private void Awake()
    {
        BtnHat.onClick.AddListener(OnClickBtnHat);
        BtnPant.onClick.AddListener(OnClickBtnPant);
        BtnShield.onClick.AddListener(OnClickBtnShield);
        BtnClothes.onClick.AddListener(OnClickBtnClothes);
        BtnExit.onClick.AddListener(OnClickBtnExit);
     
        BtnBuy.onClick.AddListener(OnClickBtnBuy);
        BtnOwned.onClick.AddListener(OnClickBtnOwned);

        Content.SetStateBuy = SetStateBuy;
    }

    private void OnClickBtnOwned()
    {
        for (int i = 0; i < Content.GetListItem().Count; i++)
        {
            if (Content.GetListItem()[i].Equipped)
            {
                Content.GetListItem()[i].SetState(true, false);
            }
        }
        Content.GetItemChosen().SetState(true, true);
        Content.GetItemChosen().SetSkinForPlayer();
        SetStateBuy(Content.GetItemChosen());
    }

    private void OnClickBtnBuy()
    {
        if(UserData.Ins.GetCoin() >= Content.GetItemChosen().price)
        {
            if(!Content.GetItemChosen().Owned)
            {
                Content.GetItemChosen().SetState(true, false);
                SetStateBuy(Content.GetItemChosen());
                UserData.Ins.SaveListSkinId(Content.GetItemChosen().id);
                UserData.Ins.SetCoin(UserData.Ins.GetCoin() - Content.GetItemChosen().price);
            }
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void Start()
    {
        OnClickBtnHat();
    }
    private void Update()
    {
        coin.text = UserData.Ins.GetCoin().ToString();
    }
    private void OnClickBtnExit()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void OnClickBtnClothes()
    {
        Content.SetSkin(SkinEnum.full);
        SetStateBuy(Content.GetListItem()[0]);
        LevelManager.Ins.GetPlayer().ResetSkin();
        ChangeColorButton(BtnClothes);
    }

    private void OnClickBtnShield()
    {
        Content.SetSkin(SkinEnum.shield);
        SetStateBuy(Content.GetListItem()[0]);
        LevelManager.Ins.GetPlayer().ResetSkin();
        ChangeColorButton(BtnShield);
    }

    private void OnClickBtnPant()
    {
        Content.SetSkin(SkinEnum.pant);
        SetStateBuy(Content.GetListItem()[0]);
        LevelManager.Ins.GetPlayer().ResetSkin();
        ChangeColorButton(BtnPant);
    }

    private void OnClickBtnHat()
    {
        Content.SetSkin(SkinEnum.hat);
        SetStateBuy(Content.GetListItem()[0]);
        LevelManager.Ins.GetPlayer().ResetSkin();
        ChangeColorButton(BtnHat);
    }

    private void ChangeColorButton(ButtonTag btn)
    {
        BtnHat.ChangeDefaultColor();
        BtnPant.ChangeDefaultColor();
        BtnShield.ChangeDefaultColor();
        BtnClothes.ChangeDefaultColor();

        btn.ChangeColorWhenClicked();
    }
    public void SetStateBuy(UICShopSkinItem i)
    {
        if(!i.Owned)
        {
            BtnBuy.gameObject.SetActive(true);
            BtnOwned.gameObject.SetActive(false);
            BtnEquipped.gameObject.SetActive(false);
        }
        else
        {
            if(!i.Equipped)
            {
                BtnBuy.gameObject.SetActive(false);
                BtnOwned.gameObject.SetActive(true);
                BtnEquipped.gameObject.SetActive(false);
            }
            else
            {
                BtnBuy.gameObject.SetActive(false);
                BtnOwned.gameObject.SetActive(false);
                BtnEquipped.gameObject.SetActive(true);
            }
        }
    }*/
    [SerializeField] private ContentShopSkin ContentHat;
    [SerializeField] private ContentShopSkin ContentPant;
    [SerializeField] private ContentShopSkin ContentShield;
    [SerializeField] private ContentShopSkin ContentFullSet;

    private ContentShopSkin curContent;

    public ButtonTag BtnTagHat;
    public ButtonTag BtnTagPant;
    public ButtonTag BtnTagShield;
    public ButtonTag BtnTagSetFull;

    public Button BtnExit;
    public Button BtnBuy;
    public Button BtnOwned;
    public Button BtnEquipped;

    public TextMeshProUGUI txtCoin;
    public TextMeshProUGUI txtItemCoin;

    private void Awake()
    {
        BtnTagHat.onClick.AddListener(OnClickBtnHat);
        BtnTagPant.onClick.AddListener(OnClickBtnPant);
        BtnTagShield.onClick.AddListener(OnClickBtnShield);
        BtnTagSetFull.onClick.AddListener(OnClickBtnClothes);
        BtnExit.onClick.AddListener(OnClickBtnExit);

        BtnBuy.onClick.AddListener(OnClickBtnBuy);
        BtnOwned.onClick.AddListener(OnClickBtnOwned);
    }
    private void Start()
    {
        txtCoin.text = UserData.Ins.GetCoin().ToString();
        curContent = ContentHat;

        ContentHat.SetPrice = CheckBtnState;
        ContentPant.SetPrice = CheckBtnState;
        ContentShield.SetPrice = CheckBtnState;
        ContentFullSet.SetPrice = CheckBtnState;


        ContentFullSet.SpawnItem();
        ContentShield.SpawnItem();
        ContentPant.SpawnItem();
        ContentHat.SpawnItem();

        OnClickBtnHat();
        CheckBtnState();
    }
    public void CheckBtnState()
    {
        if(ContentHat.gameObject.active)
        {
            if (UserData.Ins.ListOwnedHatId.Contains(ContentHat.GetChosenItem().id))
            {
                if (ContentHat.GetChosenItem().CheckEquipped())
                {
                    ActiveBtnEquipped();
                }
                else
                {
                    ActiveBtnOwned();
                }
            }
            else
            {
                txtItemCoin.text = ContentHat.GetChosenItem().price.ToString();
                if (curContent.GetChosenItem().price <= UserData.Ins.GetCoin())
                {
                    txtItemCoin.color = Color.white;
                }
                else
                {
                    txtItemCoin.color = Color.red;
                }
                ActiveBtnBuy();
            }
        }
        if (ContentPant.gameObject.active)
        {
            if (UserData.Ins.ListOwnedPantId.Contains(ContentPant.GetChosenItem().id))
            {
                if (ContentPant.GetChosenItem().CheckEquipped())
                {
                    ActiveBtnEquipped();
                }
                else
                {
                    ActiveBtnOwned();
                }
            }
            else
            {
                txtItemCoin.text = ContentPant.GetChosenItem().price.ToString();
                if (curContent.GetChosenItem().price <= UserData.Ins.GetCoin())
                {
                    txtItemCoin.color = Color.white;
                }
                else
                {
                    txtItemCoin.color = Color.red;
                }
                ActiveBtnBuy();
            }
        }
        if(ContentShield.gameObject.active)
        {
            if (UserData.Ins.ListOwnedShieldId.Contains(ContentShield.GetChosenItem().id))
            {
                if (ContentShield.GetChosenItem().CheckEquipped())
                {
                    ActiveBtnEquipped();
                }
                else
                {
                    ActiveBtnOwned();

                }
            }
            else
            {
                txtItemCoin.text = ContentShield.GetChosenItem().price.ToString();
                if (curContent.GetChosenItem().price <= UserData.Ins.GetCoin())
                {
                    txtItemCoin.color = Color.white;
                }
                else
                {
                    txtItemCoin.color = Color.red;
                }
                ActiveBtnBuy();
            }
        }
        if(ContentFullSet.gameObject.active)
        {
            if (UserData.Ins.ListOwnedFullSetId.Contains(ContentFullSet.GetChosenItem().id))
            {
                if (ContentFullSet.GetChosenItem().CheckEquipped())
                {
                    ActiveBtnEquipped();
                }
                else
                {
                    ActiveBtnOwned();
                }
            }
            else
            {
                txtItemCoin.text = ContentFullSet.GetChosenItem().price.ToString();
                if (curContent.GetChosenItem().price <= UserData.Ins.GetCoin())
                {
                    txtItemCoin.color = Color.white;
                }
                else
                {
                    txtItemCoin.color = Color.red;
                }
                ActiveBtnBuy();
            }
        }

        
    }
    private void OnClickBtnOwned()
    {
        ActiveBtnEquipped();
        UserData.Ins.SetIdEquippedSkin(curContent.GetChosenItem().id);
        UserData.Ins.SetSkinType(curContent.skinType);
    }
    private void OnClickBtnBuy()
    {
        if(UserData.Ins.GetCoin() >= curContent.GetChosenItem().price)
        {
            UserData.Ins.SetCoin(UserData.Ins.GetCoin() - (int)curContent.GetChosenItem().price);
            UserData.Ins.SaveListSkinId(curContent.GetChosenItem().id, curContent.skinType);
            curContent.GetChosenItem().Lock.gameObject.SetActive(false);
            ActiveBtnOwned();
        }
        txtCoin.text = UserData.Ins.GetCoin().ToString();
    }


    public void ActiveBtnBuy()
    {
        CloseAllBtn();
        BtnBuy.gameObject.SetActive(true);
    }
    public void ActiveBtnOwned()
    {
        CloseAllBtn();
        BtnOwned.gameObject.SetActive(true);
    }
    public void ActiveBtnEquipped()
    {
        CloseAllBtn();
        LevelManager.Ins.GetPlayer().BuffSkin(curContent.GetChosenItem().id, curContent.skinType);
        BtnEquipped.gameObject.SetActive(true);
    }


    private void OnClickBtnExit()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }


    private void OnClickBtnPant()
    {
        CloseAllTag();
        BtnTagPant.ChangeColorWhenClicked();
        ContentPant.gameObject.SetActive(true);
        curContent = ContentPant;
        ContentPant.SetBtn?.Invoke();
        curContent.SelectFirstItem();

    }

    private void OnClickBtnClothes()
    {
        CloseAllTag();
        BtnTagSetFull.ChangeColorWhenClicked();
        ContentFullSet.gameObject.SetActive(true);
        curContent = ContentFullSet;
        ContentFullSet.SetBtn?.Invoke();
        curContent.SelectFirstItem();

    }

    private void OnClickBtnShield()
    {
        CloseAllTag();
        BtnTagShield.ChangeColorWhenClicked();
        ContentShield.gameObject.SetActive(true);
        curContent = ContentShield;
        ContentShield.SetBtn?.Invoke();
        curContent.SelectFirstItem();

    }

    private void OnClickBtnHat()
    {
        CloseAllTag();
        BtnTagHat.ChangeColorWhenClicked();
        ContentHat.gameObject.SetActive(true);
        ContentHat.SetBtn?.Invoke();
        curContent = ContentHat;
        curContent.SelectFirstItem();

    }
    public void CloseAllTag()
    {
        ContentHat.gameObject.SetActive(false);
        ContentPant.gameObject.SetActive(false);
        ContentShield.gameObject.SetActive(false);
        ContentFullSet.gameObject.SetActive(false);

        BtnTagHat.ChangeDefaultColor(); 
        BtnTagPant.ChangeDefaultColor(); 
        BtnTagShield.ChangeDefaultColor(); 
        BtnTagSetFull.ChangeDefaultColor(); 
    }
    public void CloseAllBtn()
    {
        BtnBuy.gameObject.SetActive(false);
        BtnOwned.gameObject.SetActive(false);
        BtnEquipped.gameObject.SetActive(false);
    }
}
