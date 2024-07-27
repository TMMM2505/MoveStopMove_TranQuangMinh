using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Objects/Map Data", order = 1)]

public class MapData : ScriptableObject
{
    public List<mapDataConfig> mapDataConfig;
    public Sprite GetSprMap(int index) => mapDataConfig[index].sprMaps;
    public Map GetPrefabMap(int index) => mapDataConfig[index].prefabMap;
}

[Serializable]
public class mapDataConfig
{
    public int Id;
    public float price;
    public Sprite sprMaps;
    public Map prefabMap;
}