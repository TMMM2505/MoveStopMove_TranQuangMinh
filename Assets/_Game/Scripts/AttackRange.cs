using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] private Character owner;
    [SerializeField] private List<Character> listEnemy = new();

    public List<Character> GetListCharacter() => listEnemy;

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
        if (!col.CompareTag("Character")) return;

        Character character = col.GetComponent<Character>();
        if (character == null) return;
        if (character == owner) return;

        owner.CharacterEnterAttackRange(character);

        character.OnDeadRemove += owner.RemoveCharacterFromList;
    }

    private void CharacterExitAttackRange(Collider col)
    {
        if (!col.CompareTag("Character")) return;
        Character character = col.GetComponent<Character>();
        if (character == null) return;
        if (character == owner) return;
        owner.CharacterExitAttackRange(character);
        character.OnDeadRemove -= owner.RemoveCharacterFromList;
    }
}
