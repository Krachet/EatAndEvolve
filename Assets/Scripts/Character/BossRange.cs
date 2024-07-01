using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRange : MonoBehaviour
{
    [SerializeField] Boss boss;

    private void Start()
    {
        boss = transform.parent.GetComponent<Boss>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boss.ChangeAnimation(Character. AnimationState.Idle);
            boss.agent.enabled = false;
            GameController.Instance.BossFight();
        }
    }
}
