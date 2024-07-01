using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IdleState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        enemy.allPropsPosition = GameController.Instance.GetSpawnedBreakableObj();
        if (enemy.levelManager.currentLevel > enemy.player.levelManager.currentLevel)
        {
            enemy.target = enemy.player.gameObject;
        }
        else
        {
            enemy.FindTarget();
        }
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.ChangeState(new PatrolState());
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
