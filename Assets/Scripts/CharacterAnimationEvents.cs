using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;
    Character characterTarget;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
        characterTarget = character.target.GetComponentInParent<Character>();

    }

    void AttackEnd()
    {
        characterTarget.SetState(Character.State.BeginDied);
        character.SetState(Character.State.RunningFromEnemy);
    }

    void ShootEnd()
    {
        characterTarget.SetState(Character.State.BeginDied);
        character.SetState(Character.State.Idle);
    }
    void DiedEnd()
    {
        character.SetState(Character.State.Died);
    }
}
