using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.GridLayoutGroup;

public class Character : GameUnit
{
    public static Character instance;

    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CapsuleCollider col;
    [SerializeField] CameraFollower camera;
    [SerializeField] protected Material defaultPantMaterial;
    [SerializeField] protected Material defaultInitalMaterial;

    [SerializeField] protected GameObject holdWeapon;
    [SerializeField] protected GameObject leftHand;
    [SerializeField] protected GameObject head;
    [SerializeField] protected GameObject RauPos;
    [SerializeField] protected GameObject body;
    [SerializeField] protected GameObject wing;
    [SerializeField] protected GameObject tail;
    [SerializeField] protected GameObject pant;
    [SerializeField] protected GameObject Initial;
    [SerializeField] protected GameObject Container;

    public AttackRange attackRange;

    protected int curWeaponId;

    protected Weapon weapon;
    protected WeaponChar weaponChar;

    protected Character target;
    protected ScoreChar prefabsScore;
    protected CircleTarget circleTarget;
    protected GameObject pmtSetFull;


    protected List<Weapon> myWeapon = new List<Weapon>();
    protected GameObject pmtHead, pmtBody, pmtPant, pmtShield, pmtWing, pmtTail;

    protected float speed;
    protected bool alive = true, setCircleTarget = true;
    protected float amountBullet = 1, score = 1;
    protected float r;
    protected float forceThrow, increase = 1.5f;
    protected string currentAnim;
    protected bool inAttackRange, canA = false, isAttack;
    protected float CoolDownTime = 1.5f;  
    protected float buffCoin;      

    public Action<Character> OnDeadRemove;
    public Action<Character> HitChar;
    public UnityAction Respawn;
    public UnityAction isDead;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnInit(Vector3 startPoint, int equippedWeaponId)
    {
        alive = true;
        amountBullet = 1;
        col = GetComponent<CapsuleCollider>();
        attackRange.transform.localScale = new Vector3(20f, 0.5f, 20f);
        r = attackRange.transform.localScale.x / 2;
        HitChar = HitTartget;

        col.enabled = true;
        gameObject.SetActive(true);

        TF.position = startPoint;
        
        ChangeWeapon(equippedWeaponId);

        ChangeAnim("IsIdle");

        attackRange.ResetListEnemy();
    }
    public void BuffWeapon(int idWeapon)
    {
        Debug.Log("Buff Weapon");
        EBuffWeapon buffWeapon = LevelManager.Ins.weaponData.ListDataWeapon[idWeapon].BuffType;
        switch (buffWeapon)
        {
            case EBuffWeapon.AttackSpeed:
                {
                    SetCoolDownTime(LevelManager.Ins.weaponData.ListDataWeapon[idWeapon].Value);
                    break;
                }

            case EBuffWeapon.Range:
                {
                    attackRange.ChangeRange(LevelManager.Ins.weaponData.ListDataWeapon[idWeapon].Value);
                    break;
                }
        } 
    }
    public void BuffSkin(int idSkin, EItemType skinType)
    {
        Debug.Log("Buff Skin");
        switch(skinType)
        {
            case EItemType.Hat:
                {
                    SkinitemBuff(GameManager.Ins.ItemDataConfig.ListHats[idSkin].data.BuffType, GameManager.Ins.ItemDataConfig.ListHats[idSkin].data.Value);
                    break;
                }
            case EItemType.Pant:
                {
                    SkinitemBuff(GameManager.Ins.ItemDataConfig.ListPants[idSkin].data.BuffType, GameManager.Ins.ItemDataConfig.ListHats[idSkin].data.Value);
                    break;
                }
            case EItemType.Shield:
                {
                    SkinitemBuff(GameManager.Ins.ItemDataConfig.ListShields[idSkin].data.BuffType, GameManager.Ins.ItemDataConfig.ListHats[idSkin].data.Value);
                    break;
                }
            case EItemType.FullSet:
                {
                    SkinitemBuff(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].data.BuffType, GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].data.Value);
                    break;
                }
        }
    }
    public void SkinitemBuff(EBuffType buffType, float value)
    {
        switch(buffType)
        {
            case EBuffType.SpeedBonus:
                {
                    SetSpeed(value);    
                    break;
                }
            case EBuffType.LargerRange:
                {
                    attackRange.ChangeRange(value);
                    break;
                }
            case EBuffType.GoldEarnPerKill:
                {
                    buffCoinPerKill(value);
                    break;
                }
            case EBuffType.GoldEarnEndMatch:
                {
                    buffCoinEndMatch(value);
                    break;
                }
        }
    }

    private void buffCoinEndMatch(float value)
    {
        score += (value + 100) / 100;
    }

    public void buffCoinPerKill(float value)
    {
        buffCoin = value;
    }
    public void ResetCharacter()
    {
        isAttack = false;
        score = 1;
        canA = true;
        StopAllCoroutines();

        // delete list enemy
        attackRange.ResetListEnemy();

        // destroy UIWeapon & weapon
        myWeapon.Clear();

        // destroy skin

        // scale
        TF.localScale = new Vector3 (1f, 1f, 1f);
        attackRange.transform.localScale = new Vector3(20f, 0.5f, 20);

        if(prefabsScore)
        {
            prefabsScore.OnDespawn();
        }
    }
    public void SetScoreHead()
    {
        prefabsScore = SimplePool.Spawn<ScoreChar>(PoolType.ScoreChar, TF.position + new Vector3(0, 3f, 0), Quaternion.identity);
        prefabsScore.TF.SetParent(TF);
    }
    public bool CheckAttack()
    {
        if(Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
        {
            canA = false;
        }
        else if(attackRange.GetListCharacter().Count > 0)
        {
            canA = true;
        }
        return canA;
    }
    protected void OnTriggerEnter(Collider item)
    {
        if(item.gameObject.CompareTag(Constants.tagSpinW) || item.gameObject.CompareTag(Constants.tagStraightW))
        {
            if(!myWeapon.Contains(item.GetComponent<Weapon>()))
            {
                alive = false;
                canA = false;
                Hitted();
            }
        }
    }

    IEnumerator DelayAttack(float time, Vector3 direction, float corner)
    {
        yield return new WaitForSeconds(time);
        if (canA)
        {
            weapon = SimplePool.Spawn<Weapon>(weapon.poolType, TF.position + TF.forward * increase * 1.5f, Quaternion.Euler(-90, 90, corner));

            if (weapon == null)
            {
                yield break;
            }

            weapon.SetOwner(this);
            weapon.Throwed(600f, direction.normalized);
            myWeapon.Add(weapon);
            isAttack = false;
        }
        else
        {
            yield break;
        }
    }
    IEnumerator CoolDown(float time)
    {
        yield return new WaitForSeconds(time);
        amountBullet = 1;
    }
    IEnumerator Dead(float time)
    {
        yield return new WaitForSeconds(time);
        if(gameObject.GetComponent<Enemy>())
        {
            OnDespawn();

        }
        canA = false;
    }
    protected void Hitted()
    {
        if (GetComponent<Player>())
        {
            UserData.Ins.SetCoin(score + UserData.Ins.GetCoin());
        }
        ChangeAnim("IsDead");

        OnDeadRemove?.Invoke(this);
        StartCoroutine(Dead(2f));

        if(prefabsScore)
        {
            Destroy(prefabsScore.gameObject);
        }

        col.enabled = false;
        rb.useGravity = false;

        if (GetComponent<Enemy>() != null)
        {
            Respawn?.Invoke();
        }
        else if (GetComponent<Player>() != null)
        {
            isDead?.Invoke();
            if (circleTarget != null)
            {
                circleTarget.RemoveFromTarget(); 
            }
        }

        for(int i = 0; i < myWeapon.Count; i++)
        {
            myWeapon[i].OnDespawn();
        }
    }
    protected void HitTartget(Character enemy)
    {
        if(GetComponent<Player>())
        {
            if(buffCoin != 0)
            {
                score += (buffCoin * score) / 100;
            }
        }
        else
        {
            score++;
        }
        if(score % 5 == 0)
        {
            float x = score / 3;
            TF.localScale += new Vector3(0.2f, 0.2f, 0.2f) * x;
            attackRange.transform.localScale += new Vector3(0.02f, 0, 0.02f) * x;
            increase += 0.2f * x;
            r = attackRange.transform.localScale.x / 2;
            if (GetComponent<Player>() != null)
            {
                camera.Change();
            }
        }
        if(prefabsScore != null)
        {
            prefabsScore.IncreaseScore(score);
        }
        
    }
    public float GetR() => r;
    public float GetScore() => score;
    public void ChangeWeapon(int WeaponId)
    {
        if (weaponChar && weapon)
        {
            weapon.OnDespawn();
            weaponChar.OnDespawn();
        }

        this.weapon = LevelManager.Ins.weaponData.GetWeapon(WeaponId);
        weaponChar = Instantiate(LevelManager.Ins.weaponData.GetWeaponChar(WeaponId), holdWeapon.transform.position, Quaternion.identity);
        weaponChar.SetParent(holdWeapon.transform, TF);

    }
    public void ChangeSkin(int idSkin, EItemType skinType)
    {
        ResetSkin();
        ResetBuff();
        switch (skinType)
        {
            case EItemType.Hat:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListHats.Count; i++)
                    {
                        if (GameManager.Ins.ItemDataConfig.ListHats[idSkin] != null && i == idSkin)
                        {
                            if (idSkin == 0)
                            {
                                pmtHead = Instantiate(GameManager.Ins.ItemDataConfig.ListHats[idSkin].hat, RauPos.transform.position, Quaternion.Euler(0,0,0));
                                pmtHead.transform.forward = RauPos.transform.forward;
                                pmtHead.transform.position = RauPos.transform.position;
                                pmtHead.transform.SetParent(RauPos.transform);
                            }
                            else
                            {
                                pmtHead = Instantiate(GameManager.Ins.ItemDataConfig.ListHats[idSkin].hat, head.transform.position, Quaternion.Euler(0, 0, 0));
                                pmtHead.transform.forward = head.transform.forward;
                                pmtHead.transform.position = head.transform.position;
                                pmtHead.transform.SetParent(head.transform);
                            }
                    }
                }
                break;
        }
            case EItemType.Pant:
            {
                for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListPants.Count; i++)
                {
                    if (GameManager.Ins.ItemDataConfig.ListPants[idSkin] != null && i == idSkin)
                    {
                        pant.gameObject.GetComponent<SkinnedMeshRenderer>().material = GameManager.Ins.ItemDataConfig.ListPants[idSkin].pant;
                    }
                }
                break;
            }
            case EItemType.Shield:
            {
                for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListShields.Count; i++)
                {
                    if (GameManager.Ins.ItemDataConfig.ListShields[idSkin] != null && i == idSkin)
                    {
                        pmtShield = Instantiate(GameManager.Ins.ItemDataConfig.ListShields[idSkin].shield, leftHand.transform.position, Quaternion.identity);
                        pmtShield.transform.SetParent(leftHand.transform);
                    }
                }
                break;
            }
            case EItemType.FullSet:
                {
                    for (int i = 0; i < GameManager.Ins.ItemDataConfig.ListFullSet.Count; i++)
                    {
                        if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin] != null && i == idSkin)
                        {
       
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].setFull)
                            {
                                //pmtSetFull = Instantiate(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].setFull, Initial.transform.position, Quaternion.identity);
                                //pmtSetFull.transform.SetParent(Container.transform);
                                Initial.GetComponent<SkinnedMeshRenderer>().material = GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].setFull;
                            }
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].hat)
                            {
                                pmtHead = Instantiate(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].hat, head.transform.position - new Vector3(0, 0.3f, 0), Quaternion.identity);
                                pmtHead.transform.forward = head.transform.forward;
                                pmtHead.transform.SetParent(head.transform);
                            }
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].pant)
                            {
                                pant.gameObject.GetComponent<SkinnedMeshRenderer>().material = GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].pant;

                            }
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].wing)
                            {
                                pmtWing = Instantiate(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].wing, wing.transform.position, Quaternion.identity);
                                pmtWing.transform.forward = wing.transform.forward;
                                pmtWing.transform.SetParent(wing.transform);

                            }
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].tail)
                            {
                                pmtTail = Instantiate(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].tail, tail.transform.position, Quaternion.identity);
                                pmtTail.transform.forward = tail.transform.forward;
                                pmtTail.transform.SetParent(tail.transform);

                            }
                            if (GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].leftHandWeapon)
                            {
                                pmtShield = Instantiate(GameManager.Ins.ItemDataConfig.ListFullSet[idSkin].leftHandWeapon, leftHand.transform.position, Quaternion.identity);
                                pmtShield.transform.SetParent(leftHand.transform);

                            }
                        }
                    }
                    break;
                }
        }
    }
    public void ResetSkin()
    {
        if(pmtHead) { Destroy(pmtHead.gameObject); }
        if(pmtWing) { Destroy(pmtWing.gameObject); }
        if(pmtTail) { Destroy(pmtTail.gameObject); }
        if (pant.gameObject.GetComponent<SkinnedMeshRenderer>().material != defaultPantMaterial) { pant.gameObject.GetComponent<SkinnedMeshRenderer>().material = defaultPantMaterial; }
        if (Initial.gameObject.GetComponent<SkinnedMeshRenderer>().material != defaultInitalMaterial) { Initial.gameObject.GetComponent<SkinnedMeshRenderer>().material = defaultInitalMaterial; }
        if(pmtShield) { Destroy(pmtShield.gameObject); }
        if(pmtSetFull)
        {
            Destroy(pmtSetFull.gameObject);
            Initial.gameObject.SetActive(true);
        }
    }
    public void ResetBuff()
    {
        buffCoin = 0;
        CoolDownTime = 1.5f;
        if(GetComponent<Player>())
        {
            speed = 1100;
        }
        else
        {
            speed = 8;
        }
    }
    public void Attack(Vector3 posEnemy)
    {
        Vector3 direction = Vector3.zero;
        float angleBetweenDeg = 0f;
        if (canA && amountBullet == 1)
        {
            Vector3 tmpTrans = new Vector3(TF.position.x, 0.5f, TF.position.z);

            direction = posEnemy - tmpTrans;
            direction.y = 0; 


            isAttack = true;
            TF.forward = direction;

            amountBullet = 0;

            {
                Vector2 directionA = new Vector2(tmpTrans.x, tmpTrans.z);
                Vector2 directionB = new Vector2(posEnemy.x, posEnemy.z);
                Vector2 kq = directionB - directionA;
                float angle = Mathf.Atan2(kq.y, kq.x);

                if (angle > Mathf.PI)
                {
                    angle -= Mathf.PI * 2;
                }
                else if (angle < -Mathf.PI)
                {
                    angle += Mathf.PI * 2;
                }

                angleBetweenDeg = angle * Mathf.Rad2Deg;
            }
            ChangeAnim("IsAttack");
            StartCoroutine(DelayAttack(0.3f, direction, angleBetweenDeg * -1));
            ChangeAnim("IsIdle");
            StartCoroutine(CoolDown(CoolDownTime));
        }
    }
    public void SetCoolDownTime(float time)
    {
        CoolDownTime -= (time * CoolDownTime)/100 ;
    }
    public virtual void SetSpeed(float time) { }
    public Vector3 FindClosetE()
    {
        int saveIndex = 0;
        float[] distance = new float[20];
        float min = 0;
        Vector3 location = Vector3.zero;
        for(int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            distance[i] = Vector3.Distance(TF.position, attackRange.GetListCharacter()[i].transform.position);
        }
        min = distance[0];
        for (int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            if (distance[i] < min)
            {
                min = distance[i];
                saveIndex = i;
            }
        }
        /*if (GetComponent<Player>())
        {
            if (target == attackRange.GetListCharacter()[saveIndex] && target != null)
            {
                setCircleTarget = false;
            }
            else
            {
                if (circleTarget != null)
                {
                    circleTarget.RemoveFromTarget();
                }
                setCircleTarget = true;
            }
        }*/
        for (int i = 0; i < attackRange.GetListCharacter().Count; i++)
        {
            if (distance[i] == min)
            {
                location = attackRange.GetListCharacter()[i].transform.position;
                target = attackRange.GetListCharacter()[i];
            }
        }

        SetCircleTarget();
        return location;
    }
    public void SetCircleTarget()
    {
        if(GetComponent<Player>() != null)
        {
            if (attackRange.GetListCharacter().Count == 0)
            {
                if (circleTarget)
                {
                    circleTarget.RemoveFromTarget();
                }
            }
            else
            {
                if (circleTarget && circleTarget.gameObject.active)
                {
                    setCircleTarget = false;
                    circleTarget.SetPosition(target);
                    Debug.Log("Set target available");
                }
                else 
                {
                    setCircleTarget = true;
                }

                if (setCircleTarget)
                {
                    circleTarget = SimplePool.Spawn<CircleTarget>(PoolType.CircleTarget, target.TF.position - new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0));
                    circleTarget.SetPosition(target);
                    setCircleTarget = false;
                    Debug.Log("Set new target ");
                }
            }
        }
    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
            /*anim.SetBool(currentAnim, false); 
            currentAnim = animName;
            anim.SetBool(currentAnim, true);*/
        }
    }

    public void CharacterEnterAttackRange(Character character)
    {
        if (attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Add(character);
        FindClosetE();
    }

    public void CharacterExitAttackRange(Character character)
    {
        if (!attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Remove(character);
        if(GetComponent<Player>() != null)
        {
            if(character == target)
            {
                circleTarget.RemoveFromTarget();
            }
        }
    }

    public void RemoveCharacterFromList(Character character)
    {
        if (!attackRange.GetListCharacter().Contains(character)) return;
        attackRange.GetListCharacter().Remove(character);
    }
    public bool IsDead()
    {
        return !alive;
    }
}

