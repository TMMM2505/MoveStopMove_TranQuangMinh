using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public WeaponDataConfig weaponData;
    public MapData mapData;
    [SerializeField] private Player player;
    [SerializeField] private float[] maxScore;

    public List<int> listIndex = new();
    public List<Enemy> listEnemy = new();

    private GameObject[] startPoint;
    private Map currentMap;
    private float maxCurrentScore;
    private int EnemyRemain = 70, indexCurMap = 0;
    private int requiredExpIndex;
    private void Start()
    {
        UserData.Ins.GetIdEquippedWeapon();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        OnInit();
    }
    private void Update()
    {
        if (listIndex.Count == 0)
        {
            SetListIndex();
        }
    }
    public void SetListIndex()
    {
        listIndex.Clear();
        for (int i = 0; i < mapData.GetPrefabMap(indexCurMap).GetStartPoint().Length; i++)
        {
            listIndex.Add(i);
        }
    }
    public void OnInit()
    {
        LoadMap(UserData.Ins.GetIndexCurrentMap());
        //Gen Character
        bool genPlayer = true;

        int r1, r2;
        for (int i = 0; i < startPoint.Length; i++)  
        {
            r1 = Random.Range(0, listIndex.Count);
            r2 = Random.Range(0, 11);
            if(genPlayer)
            {
                player.OnInit(startPoint[listIndex[r1]].transform.position, UserData.Ins.GetIdEquippedWeapon());
                genPlayer = false;
                player.isDead = OnEndGame;
            }
            else
            {
                Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Char, startPoint[listIndex[r1]].transform.position, Quaternion.identity);
                SetEnemy(enemy, r1, r2);
                listEnemy.Add(enemy);
            }
            listIndex.RemoveAt(r1);
        }
    }
    public void ChangeWeaponForPlayer(int id)
    {
        player.ChangeWeapon(id);
    }
    public void OnStartGame()
    {
        for(int i = 0; i < listEnemy.Count; i++)
        {
            //listEnemy[i].ChangeState(new PatrolState());
            listEnemy[i].SetScoreHead();
        }
        player.SetScoreHead();
        player.ChangeAnim("IsIdle");
    }
    public void OnEndGame()
    {
        GameManager.Ins.ChangeState(GameState.EndGame);
    }
    public void Replay()
    {
        EnemyRemain = 70;
        StopAllCoroutines();

        //Reset character
        player.ResetCharacter();
        for (int i = 0; i < listEnemy.Count; i++)
        {
            if (listEnemy[i] != null)
            {
                listEnemy[i].ResetCharacter();
                listEnemy[i].OnDespawn();
            }
        }

        //Destroy Bot
        listEnemy.Clear();

        SetListIndex();
    }
    public void SetScoreForCharacter()
    {
        player.SetScoreHead();
        for (int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].SetScoreHead();
        }
    }
    public void DeactivePlayer()
    {
        player.gameObject.SetActive(false);
    }
    public void DeactiveEnemy()
    {
        for(int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].DeActive();
        }
    }
    public void ActivePlayer()
    {
        player.gameObject.SetActive(true);
        player.ChangeAnim("IsIdle");
    }
    public void ActiveEnemy()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].ChangeState(new PatrolState());
        }
    }
    public void LoadMap(int indexMap)
    {
        if (currentMap != null)
        {
            Destroy(currentMap.gameObject);
        }
        if(indexMap < mapData.mapDataConfig.Count) 
        {
            currentMap = Instantiate(mapData.GetPrefabMap(indexMap));
            indexCurMap = indexMap;

            startPoint = mapData.GetPrefabMap(indexMap).GetStartPoint();
            SetListIndex();
            currentMap.gameObject.SetActive(true);
        }
        SetrequiredExpIndex();
        maxCurrentScore = maxScore[requiredExpIndex];
    }
    public float GetPlayerScore()
    {
        return player.GetScore();
    }
    public void Respawn() 
    {
        float r = Random.Range(2, 5);
        StartCoroutine(DelayRespawn(r));
    }
    IEnumerator DelayRespawn(float time)
    {
        yield return new WaitForSeconds(time);
        int r1 = Random.Range(0, listIndex.Count);
        int r2 = Random.Range(0, 11);
        if(EnemyRemain > 0)
        {
            Enemy enemy = SimplePool.Spawn<Enemy>(PoolType.Char, startPoint[r1].transform.position, Quaternion.identity);
            if(enemy != null)
            {
                SetEnemy(enemy, r1, r2);
                enemy.ChangeState(new PatrolState());
                listIndex.RemoveAt(r1);
            }
        }
        else if(!player.IsDead())
        {
            GameManager.Ins.ChangeState(GameState.EndGame);
            player.ChangeAnim("IsWin");
        }
    }
    private void SetEnemy(Enemy enemy, int r1, int r2)
    {
        if(enemy != null)
        {
            enemy.OnInit(startPoint[listIndex[r1]].transform.position,  r2);
            enemy.EnemyOnInit();
            enemy.Respawn = Respawn;
            EnemyRemain--;
        }
    }
    public int GetEnemyRemain() => EnemyRemain;
    public bool Victory() => (EnemyRemain == 0 && !player.IsDead());
    public Player GetPlayer() => player;
    public float GetMaxScore() => maxScore[UserData.Ins.lastMapIndex];
    public void SetrequiredExpIndex()
    {
        requiredExpIndex = UserData.Ins.lastMapIndex;
    }
    public bool CheckMileStone1() => UserData.Ins.GetCurrentExp() >= maxScore[indexCurMap] / 3;
    public bool CheckMileStone2() => UserData.Ins.GetCurrentExp() >= (maxScore[indexCurMap] * 2) / 3;
}
