using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Map[] maps;
    [SerializeField] private GameObject[] startPoint;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Player player;

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
        bool genPlayer = true;
        List<Weapon> weapons = new List<Weapon>();
        List<GameObject> weaponAnim = new List<GameObject>();

        weapons = weaponData.weapons;
        weaponAnim = weaponData.weaponsAnim;

        int r;
        for (int i = 0; i < 12; i++)  
        {
            r = Random.Range(0, weapons.Count);
            if(genPlayer)
            {
                player.OnInit(weaponAnim[r], weapons[r]);
                weapons.RemoveAt(r);
                weaponAnim.RemoveAt(r);
                genPlayer = false;
            }
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Char, startPoint[r].transform.position, Quaternion.identity);
            enemy.OnInit(weaponAnim[r], weapons[r]);
            enemy.EnemyOnInit();
            weapons.RemoveAt(r);
            weaponAnim.RemoveAt(r);
        }

    }
}
