using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMap : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private Image Lock;
    [SerializeField] private Image Frame;
    public Button btn;
    public Action<ItemMap> ClickMap;
    private int id;
    private float price;
    private void Awake()
    {
        btn.onClick.AddListener(OnClickMap);
    }
    public bool CheckLock()
    {
        bool check = true;
        UserData.Ins.GetListOwnedMap();
        if (UserData.Ins.ListOwnedMaps.Contains(id) || CanBuy() && UserData.Ins.lastMapIndex + 1 == GetId())
        {
            Lock.gameObject.SetActive(false);
            check = false;
        }
        else { Lock.gameObject.SetActive(true); }

        return check;
    }
    public bool CanBuy() => (UserData.Ins.GetCurrentExp() >= LevelManager.Ins.GetMaxScore());
    private void OnClickMap()
    {
        /*if(CheckLock())
        {
            ClickMap?.Invoke(this);
        }
        else
        { Lock.gameObject.SetActive(false); }*/
        ClickMap?.Invoke(this);
    }
    public void ActiveFrame()
    {
        Frame.gameObject.SetActive(true);
    }
    public void DeactiveFrame()
    {
        Frame.gameObject.SetActive(false);
    }
    public void SetUp(Sprite sprite, Transform parent, int id, float price)
    {
        img.sprite = sprite;
        transform.SetParent(parent);
        this.id = id;
        this.price = price;
        CheckLock();
        if(id == UserData.Ins.GetIndexCurrentMap())
        {
            ActiveFrame();
        }
    }
    public float GetPrice() => price;
    public int GetId() => id;
}
