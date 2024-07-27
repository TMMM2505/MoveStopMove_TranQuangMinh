using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContentShopSkin : UICanvas
{
    public Transform Parent;
    public TextMeshProUGUI txtDescription;
    public EItemType skinType;
    private List<UICShopSkinItem> listItem = new();
    private UICShopSkinItem chosenItem = new();
    private UICShopSkinItem lastItem = new();
    public Action SetBtn, SetPrice;
    private void Start()
    {
        SetBtn = SpawnItem;
    }
    public UICShopSkinItem GetChosenItem() => chosenItem;
    public void SelectItem(UICShopSkinItem selectedItem)
    {
        LevelManager.Ins.GetPlayer().ResetSkin();
        chosenItem = selectedItem;
        txtDescription.text = chosenItem.description;
        SetPrice?.Invoke();
        LevelManager.Ins.GetPlayer().ChangeSkin(chosenItem.id, skinType);
        for(int i = 0; i < listItem.Count; i++)
        {
            if (listItem[i] == selectedItem)
            {
                if(lastItem != chosenItem)
                {
                    listItem[i].ActiveFrame();
                    lastItem = chosenItem;
                }
            }
            else
            {
                listItem[i].DeActiveFrame();
            }
        }
    }
    public void SpawnItem()
    {
        for(int i = 0; i < listItem.Count; i++)
        {
            Destroy(listItem[i].gameObject);
        }
        listItem.Clear();
        switch(skinType)
        {
            case EItemType.Hat:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListHats.Count; i++)
                    {
                        ListHat item = GameManager.Ins.ItemDataConfig.ListHats[i];
                        UICShopSkinItem tmpItem = SimplePool.Spawn<UICShopSkinItem>(PoolType.ItemSkinShop, transform.position, Quaternion.identity);
                        tmpItem.transform.SetParent(Parent);
                        tmpItem.SetUp(item.data);
                        tmpItem.OnClick = SelectItem;
                        if(CheckContainsItem(tmpItem.id))
                        {
                            tmpItem.Lock.gameObject.SetActive(false);
                        }
                        listItem.Add(tmpItem);
                    }
                    SelectItem(listItem[0]);
                    break;
                }
            case EItemType.Pant:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListPants.Count; i++)
                    {
                        ListPant item = GameManager.Ins.ItemDataConfig.ListPants[i];
                        UICShopSkinItem tmpItem = SimplePool.Spawn<UICShopSkinItem>(PoolType.ItemSkinShop, transform.position, Quaternion.identity);
                        tmpItem.transform.SetParent(Parent);
                        tmpItem.SetUp(item.data);
                        tmpItem.OnClick = SelectItem;
                        if (CheckContainsItem(tmpItem.id))
                        {
                            tmpItem.Lock.gameObject.SetActive(false);
                        }
                        listItem.Add(tmpItem);
                    }
                    SelectItem(listItem[0]);
                    break;
                }
            case EItemType.Shield:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListShields.Count; i++)
                    {
                        ListShield item = GameManager.Ins.ItemDataConfig.ListShields[i];
                        UICShopSkinItem tmpItem = SimplePool.Spawn<UICShopSkinItem>(PoolType.ItemSkinShop, transform.position, Quaternion.identity);
                        tmpItem.transform.SetParent(Parent);
                        tmpItem.SetUp(item.data);
                        tmpItem.OnClick = SelectItem;
                        if (CheckContainsItem(tmpItem.id))
                        {
                            tmpItem.Lock.gameObject.SetActive(false);
                        }
                        listItem.Add(tmpItem);
                    }
                    SelectItem(listItem[0]);
                    break;
                }
            case EItemType.FullSet:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListFullSet.Count; i++)
                    {
                        ListFullSet item = GameManager.Ins.ItemDataConfig.ListFullSet[i];
                        UICShopSkinItem tmpItem = SimplePool.Spawn<UICShopSkinItem>(PoolType.ItemSkinShop, transform.position, Quaternion.identity);
                        tmpItem.transform.SetParent(Parent);
                        tmpItem.SetUp(item.data);   
                        tmpItem.OnClick = SelectItem;
                        if (CheckContainsItem(tmpItem.id))
                        {
                            tmpItem.Lock.gameObject.SetActive(false);
                        }
                        listItem.Add(tmpItem);
                    }
                    SelectItem(listItem[0]);
                    break;
                }
        }    
        
    }
    public bool CheckContainsItem(int id)
    {
        bool check = false;
        switch (skinType)
        {
            case EItemType.Hat:
                {
                    if(UserData.Ins.ListOwnedHatId.Contains(id))
                    {
                        check = true;
                    }
                    break;
                }
            case EItemType.Pant:
                {
                    if (UserData.Ins.ListOwnedPantId.Contains(id))
                    {
                        check = true;
                    }
                    break;
                }
            case EItemType.Shield:
                {
                    if (UserData.Ins.ListOwnedShieldId.Contains(id))
                    {
                        check = true;
                    }
                    break;
                }
            case EItemType.FullSet:
                {
                    if (UserData.Ins.ListOwnedFullSetId.Contains(id))
                    {
                        check = true;
                    }
                    break;
                }
        }
        return check;
    }
}
