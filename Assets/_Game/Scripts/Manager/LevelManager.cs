using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Map[] maps;
    [SerializeField] private GameObject[] startPoint;
    
    private Character character;
    private int indexCurMap = 0;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        //Gen Map
        maps[indexCurMap].SetActive();
        //Gen Character
        for(int i = 0; i < 1; i++)
        {
            character = SimplePool.Spawn<Enemy>(PoolType.Char, startPoint[i].transform.position, Quaternion.identity);

        }
    }
}
