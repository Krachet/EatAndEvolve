using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public float health;
    private float lerpSpeed;
    public float maxHealth;
    public float chipSpeed;
    public Image frontHP;
    public Image backHP;

    public void OnInit()
    {
        health = maxHealth;
        frontHP.fillAmount = 1;
        backHP.fillAmount = 1;
    }

    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float frontFill = frontHP.fillAmount;
        float backFill = backHP.fillAmount;
        float hFraction = health / maxHealth;
        if (backFill > hFraction)
        {
            frontHP.fillAmount = hFraction;
            backHP.color = Color.red;
            lerpSpeed += Time.deltaTime * chipSpeed;
            float percentComplete = lerpSpeed / chipSpeed;
            backHP.fillAmount = Mathf.Lerp(backFill, hFraction, percentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpSpeed = 0;
    }
}
