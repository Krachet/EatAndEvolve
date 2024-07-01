using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Character
{
    public NavMeshAgent agent;

    public Player player;

    public GameObject range;
    public GameObject lootRange;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        agent.enabled = true;
        range.GetComponent<Collider>().enabled = true;
        lootRange.GetComponent<Collider>().enabled = true;
        levelManager.currentLevel = stats.level;
        player = FindAnyObjectByType<Player>();
        agent.speed = 4f;
        ChangeAnimation(AnimationState.Run);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }
        Vector3 lookAt = Vector3.Lerp(transform.position, player.transform.position, 0.2f);
        transform.LookAt(lookAt);
    }

    public override void OnDie()
    {
        base.OnDie();
        gameObject.GetComponent<Collider>().enabled = false;
        range.GetComponent<Collider>().enabled = false;
        lootRange.GetComponent<Collider>().enabled = false;
        GameController.Instance.isFinalBoss = true;
        ChangeAnimation(AnimationState.Dead);
        Invoke(nameof(ReturnToPool), 2f);
    }

    private void ReturnToPool()
    {
        player.levelManager.AddLevel(levelManager.currentLevel);
        Destroy(gameObject);
        GameController.Instance.OnFinalBoss();
    }
}
