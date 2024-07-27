using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICChoseMap : UICanvas
{
    [SerializeField] private ProcessBar processBar;
    [SerializeField] private Transform parent;
    [SerializeField] private ItemMap prefabItemMap;
    public Button btnExit;
    public Button btnBuy;
    public Button btnOwned;
    public Button btnEquiped;
    public TextMeshProUGUI txtPrice;
    public TextMeshProUGUI txtCoin;
    private List<ItemMap> listMap = new();

    private ItemMap chosenMap;
    private void Awake()
    {
        btnExit.onClick.AddListener(OnClickBtnExit);
        btnBuy.onClick.AddListener(OnClickBtnBuy);
        btnOwned.onClick.AddListener(OnClickBtnOwned);
    }

    private void OnClickBtnOwned()
    {
        ActiveBtnEquipped();
        UserData.Ins.SetListMap(chosenMap.GetId());
        if (chosenMap.GetId() != UserData.Ins.GetIndexCurrentMap())
        {
            Debug.Log("check");
            UserData.Ins.SetIndexCurrentMap(chosenMap.GetId());
            LevelManager.Ins.Replay();
            LevelManager.Ins.OnInit();
        }
    }

    private void OnClickBtnBuy()
    {
        if(chosenMap.CanBuy() && UserData.Ins.lastMapIndex + 1 == chosenMap.GetId())
        {
            if (UserData.Ins.GetCoin() >= chosenMap.GetPrice())
            {
                txtPrice.color = Color.white;
                ActiveBtnOwned();
                UserData.Ins.SetListMap(chosenMap.GetId());
                UserData.Ins.SetCoin(UserData.Ins.GetCoin() - chosenMap.GetPrice());
                UserData.Ins.SetCurrentExp(0);
                txtCoin.text = UserData.Ins.GetCoin().ToString();
                SetProcessBar();
            }
        }
        else
        {
            txtPrice.color = Color.red;
        }
    }

    private void OnClickBtnExit()
    {
        Close(0);
        GameManager.Ins.ChangeState(GameState.MainMenu);
    }

    private void Start()
    {
        SetUp();
        txtCoin.text = UserData.Ins.GetCoin().ToString();
        processBar.OnInit(LevelManager.Ins.GetMaxScore(), UserData.Ins.GetCurrentExp());
        SetProcessBar();
    }
    public void SetProcessBar()
    {
        processBar.SetBarChoseMap();
        processBar.SetColorBG();
    }
    public void ClickMap(ItemMap chosenMap)
    {
        this.chosenMap = chosenMap;
        for(int i = 0; i < listMap.Count; i++)
        {
            if (listMap[i] == chosenMap)
            {
                listMap[i].ActiveFrame();
                txtPrice.text = LevelManager.Ins.mapData.mapDataConfig[i].price.ToString();

            }
            else
            {
                listMap[i].DeactiveFrame();
            }
        }
        if (UserData.Ins.ListOwnedMaps.Contains(chosenMap.GetId()))
        {
            if (UserData.Ins.GetIndexCurrentMap() == chosenMap.GetId())
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

            ActiveBtnBuy();
        }
        if (chosenMap.CanBuy() && UserData.Ins.lastMapIndex + 1 == chosenMap.GetId())
        {
            if (UserData.Ins.GetCoin() >= chosenMap.GetPrice())
            {
                txtPrice.color = Color.white;
            }
        }
        else
        {
            txtPrice.color = Color.red;
        }
    }
    public void SetUp()
    {
        for(int i = 0; i < LevelManager.Ins.mapData.mapDataConfig.Count; i++)
        {
            ItemMap iMap = Instantiate(prefabItemMap, parent.position, Quaternion.identity);
            iMap.SetUp(LevelManager.Ins.mapData.GetSprMap(i), parent, LevelManager.Ins.mapData.mapDataConfig[i].Id, LevelManager.Ins.mapData.mapDataConfig[i].price);
            iMap.ClickMap = ClickMap;
            listMap.Add(iMap);
        }
        ClickMap(listMap[0]);
    }
    public void ActiveBtnBuy()
    {
        btnBuy.gameObject.SetActive(true);
        btnOwned.gameObject.SetActive(false);
        btnEquiped.gameObject.SetActive(false);
    }
    public void ActiveBtnOwned()
    {
        btnBuy.gameObject.SetActive(false);
        btnOwned.gameObject.SetActive(true);
        btnEquiped.gameObject.SetActive(false);
    }
    public void ActiveBtnEquipped()
    {
        btnBuy.gameObject.SetActive(false);
        btnOwned.gameObject.SetActive(false);
        btnEquiped.gameObject.SetActive(true);
    }
    public void SetUpPerOpening()
    {
        for(int i = 0; i < listMap.Count; i++)
        {
            if (listMap[i].CanBuy() && UserData.Ins.lastMapIndex + 1 == listMap[i].GetId())
            {
                listMap[i].CheckLock();
            }
        }
    }
}
