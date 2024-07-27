using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Experimental.AI;
using static UnityEditor.Progress;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private List<Character> listEnemy = new();

    public List<Character> GetListCharacter() => listEnemy;

    private Obstacles lastObstacles;
    private void Update()
    {
        RemoveWhenDie();
    }

    protected void OnTriggerEnter(Collider item)
    {
        CollideWithCharacter(item);

        if(owner.GetComponent<Player>() && item.gameObject.CompareTag(Constants.tagObstacles))
        {
            EnterObstacles(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterExitAttackRange(other);
        if (owner.GetComponent<Player>() && other.gameObject.CompareTag(Constants.tagObstacles))
        {
            ExitObstacles(other);
        }
    }

    private void CollideWithCharacter(Collider col)
    {
        Character c = CacheDictionary.GetComponentFromCache<Character>(col);
        if (c == null) return;
        if (c == owner) return;
        owner.CharacterEnterAttackRange(c);
        c.OnDeadRemove += owner.RemoveCharacterFromList;
    }
    private void EnterObstacles(Collider col)
    {
        Obstacles obs = col.GetComponent<Obstacles>();
        if (obs != null)
        {
            lastObstacles = obs;
            obs.BeTransparent();
        }
    }
    private void ExitObstacles(Collider col)
    {
        if (lastObstacles != null)
        {
            lastObstacles.ResetAlpha();
        }
    }
    private void CharacterExitAttackRange(Collider col)
    {
        Character c = CacheDictionary.GetComponentFromCache<Character>(col);

        if (c == null) return;
        if (c == owner) return;
        owner.CharacterExitAttackRange(c);
        c.OnDeadRemove -= owner.RemoveCharacterFromList;
    }
    private void RemoveWhenDie()
    {
        for(int i = 0;i < listEnemy.Count; i++) 
        {
            if (listEnemy[i].IsDead() && listEnemy[i] != null)
            {
                Debug.Log("check");
                listEnemy[i].OnDeadRemove -= owner.RemoveCharacterFromList;
                listEnemy.RemoveAt(i);
            }
        }
    }
    public void ResetListEnemy()
    {
        listEnemy.Clear();
    }
    public void ChangeRange(float time)
    {
        float x = (time + 100) / 100;
        transform.localScale += new Vector3(x, 0, x);
    }
}
