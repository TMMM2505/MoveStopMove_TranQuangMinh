using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private List<Character> listEnemy = new();

    public List<Character> GetListCharacter() => listEnemy;
    private void Update()
    {
        RemoveWhenDie();
    }

    protected void OnTriggerEnter(Collider item)
    {
        CollideWithCharacter(item);
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterExitAttackRange(other);
    }

    private void CollideWithCharacter(Collider col)
    {
        Character c = CacheDictionary.GetComponentFromCache<Character>(col);
        if (c == null) return;
        if (c == owner) return;
        owner.CharacterEnterAttackRange(c);
        c.OnDeadRemove += owner.RemoveCharacterFromList;
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
}
