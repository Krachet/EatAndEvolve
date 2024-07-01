using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjects : MonoBehaviour, IDamageable
{
    public BreakableObjectStats stats;
    //player
    private List<Character> beingHitBy = new List<Character>();
    private Character whoGetExp;
    public int goldDrop;

    public Canvas healthbarCanvas;

    public Healthbar healthbarPf;
    public Healthbar healthbar;

    //HitFX
    public GameObject hitFX;
    public GameObject bloodFX;

    public int lootChance;
    public int lootAmount;

    [SerializeField] GameObject dropPf;


    public void OnInit()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        healthbar = Instantiate(healthbarPf, healthbarCanvas.transform.position, Quaternion.identity, healthbarCanvas.transform);
        healthbar.maxHealth = stats.health;
        healthbar.OnInit();
        GameController.Instance.spawnedBreakableObj.Add(gameObject);
        lootAmount = stats.foodDrop;
    }
    public void OnTakeDamage(float Damage)
    {
        healthbar.TakeDamage(Damage);

        if (healthbar.health <= 0.1f)
        {
            OnDie();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hands")
        {
            whoGetExp = other.gameObject.GetComponent<Hands>().GetParent();
            OnTakeDamage(whoGetExp.attackDamage);
            EffectController.Instance.PlayEffect(hitFX, other.transform);
        }

        if (other.gameObject.tag == "Range")
        {
            beingHitBy.Add(other.transform.parent.GetComponent<Character>());
        }
    }

    private void OnDie()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        foreach (Character character in beingHitBy)
        {
            character.targets.Remove(gameObject);
            character.target = null;
        }
        GameController.Instance.spawnedBreakableObj.Remove(gameObject);
        for (int i = 0; i < lootAmount; i++)
        {
            GameObject dropFromBreak = Instantiate(dropPf, transform.position, Quaternion.identity);
        }
        EffectController.Instance.PlayEffect(bloodFX, transform);
        Destroy(gameObject.transform.parent.gameObject, 0.1f);
    }
}
