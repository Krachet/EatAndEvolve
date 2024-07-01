using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        enemy.isDead = true;
        enemy.counter.Start(() => OnExit(enemy), 3f);
        enemy.player.targets.Remove(enemy.hitbox);
        enemy.killer.targets.Remove(enemy.hitbox);
        enemy.range.GetComponent<Collider>().enabled = false;
        enemy.lootRange.GetComponent<Collider>().enabled = false;
        enemy.hitbox.GetComponent<Collider>().enabled = false;
        enemy.targets.Clear();
        enemy.agent.enabled = false;
        enemy.ChangeAnimation(Character.AnimationState.Dead);
        enemy.PushAway();
        enemy.gameObject.GetComponent<Collider>().enabled = false;
        enemy.killer.levelManager.AddLevel(enemy.levelManager.currentLevel);
        GameController.Instance.spawnedEnemy.Remove(enemy.gameObject);
        if (GameController.Instance.spawnedEnemy.Count == 0)
        {
            GameController.Instance.OnFinalBoss();
        }
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.counter.Execute();
    }

    public void OnExit(Enemy enemy)
    {
        enemy.isDead = false;
    }
}
