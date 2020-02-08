using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        BeginDied,
        Died,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist,
    }

    public float runSpeed;
    public float distanceFromEnemy;
    public Transform target;
    public Weapon weapon;
    public string TargetState;
    Animator animator;
    Animator animatorEnemy;
    Vector3 originalPosition;
    Quaternion originalRotation;
    State state = State.Idle;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animatorEnemy = target.GetComponentInChildren<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        if (target.GetComponentInParent<Character>().state == State.Died )
        {
            print("even died");
            return; 
        }

        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;

            case Weapon.Fist:
                state = State.RunningToEnemy;
                break;

            case Weapon.Pistol:
                state = State.BeginShoot;
                break;
        }
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state) {
            case State.Idle:
                animator.SetFloat("speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy))
                    state = State.BeginAttack;
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.BeginAttack:
                animator.SetFloat("speed", 0.0f);
                //animator.SetTrigger("attack");
                switch (weapon)
                {
                    case Weapon.Bat:
                        animator.SetTrigger("attack");
                        break;

                    case Weapon.Fist:
                        animator.SetTrigger("attackZombie");
                        break;
                }
                state = State.Attack;
                break;

            case State.Attack:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.BeginShoot:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("shoot");
                state = State.Shoot;
                break;

            case State.Shoot:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.BeginDied:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("died");
                state = State.Died;
                break;

            case State.Died:
                animator.SetFloat("speed", 0.0f);

                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 vector = direction * runSpeed;
        if (vector.magnitude < distance.magnitude) {
            transform.position += vector;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

}
