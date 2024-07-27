using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICShopSkinItem : GameUnit
{
    /*public GameObject Lock;

    public Image FrameChosen;
    public int id, price;
    public string destription;
    public SkinEnum skinEnum;

    public Action<UICShopSkinItem> onClick;

    private Button thisBtn;
    public bool Owned, Equipped;
    private void Start()
    {
        thisBtn = GetComponent<Button>();
        thisBtn.onClick.AddListener(Clicked);
        
    }
    public void Clicked()
    {
        onClick?.Invoke(this);
    }
    public void UnClicked()
    {
        FrameChosen.gameObject.SetActive(false);
    }
    public void OnClicked()
    {
        FrameChosen.gameObject.SetActive(true);
    }
    public void SetLock()
    {
        if(Owned)
        {
            Lock.gameObject.SetActive(false);
        }
        else
        {
            Lock.gameObject.SetActive(true);
        }
    }
    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.forward = parent.forward;
    }
    public void SetState(bool Owned, bool Equipped)
    {
        this.Owned = Owned;
        this.Equipped = Equipped;
    }
    public void SetSkinForPlayer()
    {
        LevelManager.Ins.GetPlayer().ChangeSkin(id, skinEnum);
    }*/

    public int id;
    public float price;
    public string description;
    public Image Lock;
    public Image Frame;
    public Image Icon;
    public Button Button;

    public Button BtnBuy;
    public Button BtnOwned;
    public Button BtnEquipped;

    public Action<UICShopSkinItem> OnClick;

    private void Awake()
    {
        Button.onClick.AddListener(ButtonOnCLick);
    }
    public void SetUp(ItemData item)
    {
        Icon.sprite = item.Icon;
        id = item.ID;
        price = item.Price;
        description = item.Description;
    }
    private void ButtonOnCLick()
    {
        OnClick?.Invoke(this);
    }
    public bool CheckEquipped() => (UserData.Ins.GetIdEquippedSkin() == id);
    public void ActiveFrame()
    {
        Frame.gameObject.SetActive(true);
    }
    public void DeActiveFrame()
    {
        Frame.gameObject.SetActive(false);
    }
}
