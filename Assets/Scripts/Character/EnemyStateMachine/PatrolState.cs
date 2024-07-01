using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Enemy>
{
    public void OnEnter(Enemy enemy)
    {
        enemy.ChangeAnimation(Character.AnimationState.Run);
        if (!enemy.target)
        {
            enemy.ChangeState(new IdleState());
        }
        Vector3 direct = enemy.transform.position - enemy.target.transform.position;
        enemy.SetDestination(enemy.target.transform.position + direct*0.1f);
        enemy.counter.Start(() => enemy.ChangeState(new IdleState()), 1f);
    }

    public void OnExecute(Enemy enemy)
    {
        enemy.Counter.Execute();
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
