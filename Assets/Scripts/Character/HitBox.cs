using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Character parent;

    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Character>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hands")
        {
            parent.killer = other.gameObject.GetComponent<Hands>().GetParent();
            //parent.OnTakeDamage(parent.killer.attackDamage);
            if (parent.killer.levelManager.currentLevel > parent.levelManager.currentLevel)
            {
                parent.OnTakeDamage(1);
            }
            EffectController.Instance.PlayEffect(parent.hitFX, other.transform);
        }
    }
}
