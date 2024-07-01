using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Canvas canvas;
    public bool isSlowed;
    public Image slow;
    public TextMeshProUGUI slowText;

    public Transform bossPOV;
    public GameObject boss;

    public bool isBossFight;
    // Start is called before the first frame update
    void Start()
    {
        healthbar.OnInit();
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBossFight)
        {
            healthbar.gameObject.SetActive(false);
            if (Input.GetMouseButtonDown(0))
            {
                ChangeAnimation(AnimationState.Push);
                levelManager.currentLevel += attackDamage;
            }
        }
        if (isSlowed)
        {
            slowText.gameObject.SetActive(true);
        }
        else
        {
            slowText.gameObject.SetActive(false);
        }
        if (isDead)
        {
            ChangeAnimation(AnimationState.Dead);
            return;
        }
        else
        {
            if (Vector3.Distance(JoystickControl.direct, Vector3.zero) < 0.1f)
            {
                ChangeAnimation(AnimationState.Idle);
            }
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
                Move();
            }
            if (targets.Count > 0)
            {
                target = targets[0];
                Attack();
            }
            else if (targets.Count == 0)
            {
                ChangeAnimation(AnimationState.NotAttack);
            }
        }
    }

    public override void OnDie()
    {
        base.OnDie();
        targets.Clear();
        isDead = true;
        GameController.Instance.OnFinish();
    }
}
