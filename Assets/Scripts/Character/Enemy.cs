using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Character
{
    //NavMeshAgent
    [Header("NavMeshAgent")]
    public NavMeshAgent agent;
    public Vector3 destination;
    public bool isAtDestination => (Mathf.Abs(destination.x - transform.position.x) + Mathf.Abs(destination.z - transform.position.z)) < 1.1f;

    //moving destination
    [Header("Moving Destination")]
    public List<GameObject> allPropsPosition = new List<GameObject>();
    float distance;

    //StateMachine
    IState<Enemy> currentState;

    public CounterTime Counter => counter;

    //Range
    public GameObject hitbox;
    public GameObject range;
    public GameObject lootRange;


    public Player player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnInit()
    {
        GameController.Instance.spawnedEnemy.Add(gameObject);
        range.GetComponent<Collider>().enabled = true;
        lootRange.GetComponent<Collider>().enabled = true;
        hitbox.GetComponent<Collider>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        agent.enabled = true;
        healthbar.maxHealth = 3;
        healthbar.OnInit();
        allPropsPosition = GameController.Instance.GetSpawnedBreakableObj();
        foodPerDrop = stats.foodPerDrop;
        levelManager.currentLevel = stats.level;
        attackDamage += stats.damage;
        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            agent.enabled = false;
            return;
        }
        else
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
            if (targets.Count < 1)
            {
                ChangeAnimation(AnimationState.NotAttack);
            }
            else if (targets.Count > 0)
            {
                if (!targets[0])
                {
                    targets.RemoveAt(0);
                    return;
                }
                Attack();
            }
        }
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        agent.SetDestination(destination);
    }

    public void FindTarget()
    {
        float lowestDist = Mathf.Infinity;
        if (allPropsPosition.Count < 1)
        {
            target = player.gameObject;
        }
        for (int i = 0; i < allPropsPosition.Count; i++)
        {
            float dist = Vector3.Distance(allPropsPosition[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                target = allPropsPosition[i];
            }
        }

    }

    public void ChangeState(IState<Enemy> newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void PushAway()
    {
        Vector3 pushDirect = player.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(-pushDirect.normalized * 10, ForceMode.Impulse);
    }
    public override void OnDie()
    {
        base.OnDie();
        ChangeState(new DeadState());
        Invoke(nameof(ReturnToPool), 5f);
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
    