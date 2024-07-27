using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserData : Singleton<UserData>
{
    public List<int> ListOwnedWeaponId = new();
    public List<int> ListOwnedHatId = new();
    public List<int> ListOwnedPantId = new();
    public List<int> ListOwnedShieldId = new();
    public List<int> ListOwnedFullSetId = new();
    public List<int> ListOwnedMaps = new();
    public int lastMapIndex;
    private void Awake()
    {
        SetCoin(9999);
        ResetOwnedId();
        GetListIdWeaponOwned();
        GetIdEquippedWeapon();
        GetListOwnedMap();

        GetListIdSkinOwned();
        GetSkinType();
        GetIdEquippedSkin();

        //SetIndexCurrentMap(1);
    }
    public void ResetOwnedId()
    {
        PlayerPrefs.DeleteKey(Constants.keyListOwnedMap);
        PlayerPrefs.DeleteKey(Constants.keyIndexCurrentMap);
        SetListMap(0);
        SetIndexCurrentMap(0);


        PlayerPrefs.DeleteKey(Constants.keyListOwnedWeapon);
        PlayerPrefs.DeleteKey(Constants.keyEquippedWeapon);

        PlayerPrefs.DeleteKey(Constants.keyListOwnedHat);
        PlayerPrefs.DeleteKey(Constants.keyListOwnedPant);
        PlayerPrefs.DeleteKey(Constants.keyListOwnedShield);
        PlayerPrefs.DeleteKey(Constants.keyListOwnedFullSet);
        PlayerPrefs.DeleteKey(Constants.keyEquippedSkin);
        PlayerPrefs.DeleteKey(Constants.keySkinTpye);
        LevelManager.Ins.GetPlayer().ResetSkin();

        PlayerPrefs.DeleteKey(Constants.KeyCurrentExp);
        SetCurrentExp(49);

        SetSound(true);
        SetVibe(false);
    }
    public void SetCoin(float coin)
    {
        PlayerPrefs.SetFloat(Constants.keyCoin, coin);
        PlayerPrefs.Save();
    }
    public void SetSound(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetInt(Constants.keyCheckOnSound, 1);
        }
        else
        {
            PlayerPrefs.SetInt(Constants.keyCheckOnSound, 0);
        }
    }
    public void SetVibe(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetInt(Constants.keyCheckOnVibe, 1);
        }
        else
        {
            PlayerPrefs.SetInt(Constants.keyCheckOnVibe, 0);
        }
    }
    public void SetIdEquippedWeapon(int id)
    {
        PlayerPrefs.SetInt(Constants.keyEquippedWeapon, id);
        PlayerPrefs.Save();
    }
    public void SetIdEquippedSkin(int id)
    {
        PlayerPrefs.SetInt(Constants.keyEquippedSkin, id);
        PlayerPrefs.Save();
    }
    public void SetSkinType(EItemType type)
    {
        PlayerPrefs.SetInt(Constants.keySkinTpye, (int)type);
    }
    public void SaveListWeaponId(int id)
    {
        Debug.Log("save to list");
        if(!ListOwnedWeaponId.Contains(id))
        {
            ListOwnedWeaponId.Add(id);
        }
        string tmp = string.Join(",", ListOwnedWeaponId);
        PlayerPrefs.SetString(Constants.keyListOwnedWeapon, tmp);
        PlayerPrefs.Save();
    }
    public void SaveListSkinId(int id, EItemType type)
    {
        switch(type)
        {
            case EItemType.Hat:
                {
                    if (!ListOwnedHatId.Contains(id))
                    {
                        ListOwnedHatId.Add(id);
                    }
                    string tmp = string.Join(",", ListOwnedHatId);
                    PlayerPrefs.SetString(Constants.keyListOwnedHat, tmp);
                    PlayerPrefs.Save();
                    break;
                }
            case EItemType.Pant:
                {
                    if (!ListOwnedPantId.Contains(id))
                    {
                        ListOwnedPantId.Add(id);
                    }
                    string tmp = string.Join(",", ListOwnedPantId);
                    PlayerPrefs.SetString(Constants.keyListOwnedPant, tmp);
                    PlayerPrefs.Save();
                    break;
                }
            case EItemType.Shield:
                {
                    if (!ListOwnedShieldId.Contains(id))
                    {
                        ListOwnedShieldId.Add(id);
                    }
                    string tmp = string.Join(",", ListOwnedShieldId);
                    PlayerPrefs.SetString(Constants.keyListOwnedShield, tmp);
                    PlayerPrefs.Save();
                    break;
                }
            case EItemType.FullSet:
                {
                    if (!ListOwnedFullSetId.Contains(id))
                    {
                        ListOwnedFullSetId.Add(id);
                    }
                    string tmp = string.Join(",", ListOwnedFullSetId);
                    PlayerPrefs.SetString(Constants.keyListOwnedFullSet, tmp);
                    PlayerPrefs.Save();
                    break;
                }
        }
    }

    public void SetListMap(int id)
    {
        if(!ListOwnedMaps.Contains(id))
        {
            ListOwnedMaps.Add(id);
            lastMapIndex = id;
            string tmp = string.Join(",", ListOwnedMaps);
            PlayerPrefs.SetString(Constants.keyListOwnedMap, tmp);
            PlayerPrefs.Save();
        }
        GetListOwnedMap();
    }

    public void SetCurrentExp(float exp)
    {
        PlayerPrefs.SetFloat(Constants.KeyCurrentExp, exp);
    }
    public void SetNewCurrentExp()
    {
        PlayerPrefs.SetFloat(Constants.KeyCurrentExp, 0);
    }


    public void SetIndexCurrentMap(int index)
    {
        PlayerPrefs.SetInt(Constants.keyIndexCurrentMap, index);
    }
    //======================================================================
    public float GetCoin() => PlayerPrefs.GetFloat(Constants.keyCoin);


    public int GetIdEquippedWeapon() => PlayerPrefs.GetInt(Constants.keyEquippedWeapon);
    public int GetIdEquippedSkin() => PlayerPrefs.GetInt(Constants.keyEquippedSkin);
    public EItemType GetSkinType() => (EItemType)PlayerPrefs.GetInt(Constants.keySkinTpye);

    public float GetCurrentExp() => PlayerPrefs.GetFloat(Constants.KeyCurrentExp);
    public int GetIndexCurrentMap() => PlayerPrefs.GetInt(Constants.keyIndexCurrentMap);

    public void GetListIdWeaponOwned()
    {
        string tmp = PlayerPrefs.GetString(Constants.keyListOwnedWeapon);
        string[] tmp2 = tmp.Split(",");
        for(int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedWeaponId.Add(int.Parse(tmp2[i]));
            }
        }
    }
    public void GetListIdSkinOwned()
    {
        string tmp = PlayerPrefs.GetString(Constants.keyListOwnedHat);
        string[] tmp2 = tmp.Split(",");
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedHatId.Add(int.Parse(tmp2[i]));
            }
        }

        tmp = PlayerPrefs.GetString(Constants.keyListOwnedPant);
        tmp2 = tmp.Split(",");
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedPantId.Add(int.Parse(tmp2[i]));
            }
        }

        tmp = PlayerPrefs.GetString(Constants.keyListOwnedShield);
        tmp2 = tmp.Split(",");
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedShieldId.Add(int.Parse(tmp2[i]));
            }
        }

        tmp = PlayerPrefs.GetString(Constants.keyListOwnedFullSet);
        tmp2 = tmp.Split(",");
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedFullSetId.Add(int.Parse(tmp2[i]));
            }
        }
    }

    public void GetListOwnedMap()
    {
        string tmp = PlayerPrefs.GetString(Constants.keyListOwnedMap);
        string[] tmp2 = tmp.Split(",");
        for (int i = 0; i < tmp2.Length; i++)
        {
            if (tmp2[i] != "")
            {
                ListOwnedMaps.Add(int.Parse(tmp2[i]));
            }
        }
    }
    public bool GetCheckOnSound() => PlayerPrefs.GetInt(Constants.keyCheckOnSound) == 1;
    public bool GetCheckOnVibe() => PlayerPrefs.GetInt(Constants.keyCheckOnVibe) == 1;
}
