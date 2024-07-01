using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    //SCRIPTABLE OBJECT
    public StatsScriptableObject stats;

    //LEVEL AND EXP
    public LevelManager levelManager;

    //ANIMATIONS
    public Animator[] animator;
    public int currentAnimator;
    
    //stats
    public Healthbar healthbar;
    public float size;

    public float speed = 5f;
    public List<GameObject> targets = new List<GameObject>();
    public bool isDead;

    public int attackDamage;

    public int foodPerDrop;

    //HitFX
    public GameObject levelUpFX;
    public GameObject hitFX;
    public GameObject bloodFX;

    public CounterTime counter = new CounterTime();

    //DropExp
    public Character killer;
    public GameObject target;

    public enum AnimationState
    {
        Idle,
        Run,
        Attack,
        NotAttack,
        Dead,
        Push,
        Finisher
    }
    public virtual void OnDie()
    {
        EffectController.Instance.PlayEffect(bloodFX, transform);
    }

    public void ChangeAnimation(AnimationState state)
    {
        switch(state)
        {
            case AnimationState.Idle:
                animator[levelManager.currentAnimator].SetBool("isIdle", true);
                animator[levelManager.currentAnimator].SetBool("isRun", false);
                animator[levelManager.currentAnimator].SetBool("isDead", false);
                break;
            case AnimationState.Run:
                animator[levelManager.currentAnimator].SetBool("isIdle", false);
                animator[levelManager.currentAnimator].SetBool("isRun", true);
                animator[levelManager.currentAnimator].SetBool("isDead", false);
                break;
            case AnimationState.Attack:
                animator[levelManager.currentAnimator].SetBool("isAttack", true);
                break;
            case AnimationState.NotAttack:
                animator[levelManager.currentAnimator].SetBool("isAttack", false);
                break;
            case AnimationState.Dead:
                animator[levelManager.currentAnimator].SetBool("isIdle", false);
                animator[levelManager.currentAnimator].SetBool("isRun", false);
                animator[levelManager.currentAnimator].SetBool("isDead", true);
                animator[levelManager.currentAnimator].SetBool("isAttack", false);
                break;
            case AnimationState.Push:
                animator[levelManager.currentAnimator].SetTrigger("Push");
                animator[levelManager.currentAnimator].SetBool("isIdle", false);
                animator[levelManager.currentAnimator].SetBool("isRun", false);
                animator[levelManager.currentAnimator].SetBool("isDead", false);
                animator[levelManager.currentAnimator].SetBool("isAttack", false);
                break;
            case AnimationState.Finisher:
                animator[levelManager.currentAnimator].SetTrigger("Finisher");
                animator[levelManager.currentAnimator].SetBool("isIdle", false);
                animator[levelManager.currentAnimator].SetBool("isRun", false);
                animator[levelManager.currentAnimator].SetBool("isDead", false);
                animator[levelManager.currentAnimator].SetBool("isAttack", false);
                break;
        }
    }

    public void Attack()
    {
        ChangeAnimation(AnimationState.Attack);
        if (target)
        {
            Vector3 hitDirection = (target.transform.position - transform.position).normalized;
            hitDirection.y = transform.position.y;
            transform.forward = hitDirection;
        }
    }

    public void Move()
    {
        Vector3 nextPos = transform.position + JoystickControl.direct;
        ChangeAnimation(AnimationState.Run);
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        transform.forward = JoystickControl.direct;
    }

    //public void StatsIncrease(float amount)
    //{
    //    maxHealth += amount + levelManager.currentLevel * 0.1f;
    //    health += amount + levelManager.currentLevel * 0.1f;
    //    attackDamage += amount * 0.1f;
    //}

    public void OnTakeDamage(float Damage)
    {
        healthbar.TakeDamage(Damage);
        if (healthbar.health < 1)
        {
            OnDie();
        }
    }
}
