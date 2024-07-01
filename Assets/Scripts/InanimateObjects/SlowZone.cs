using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlowZone : MonoBehaviour
{
    private bool isSlowed;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (isSlowed)
        {
            player.speed = 2f;
            StartCoroutine(FillImage(player));
        }
        else
        {
            player.speed = 5f;
            StartCoroutine(UnfillImage(player));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isSlowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isSlowed = false;
        }
    }

    public IEnumerator FillImage(Player player)
    {
        yield return new WaitForSeconds(0.1f);
        player.slow.fillAmount = Mathf.Lerp(player.slow.fillAmount, player.slow.fillAmount + 0.1f, 0.5f);
        if (player.slow.fillAmount < 1)
        {
            StartCoroutine(FillImage(player));
        }
        else
        {
            player.isSlowed = true;
        }
    }

    public IEnumerator UnfillImage(Player player)
    {
        yield return new WaitForSeconds(0.1f);
        player.slow.fillAmount = Mathf.Lerp(player.slow.fillAmount, player.slow.fillAmount - 0.1f, 0.5f);
        if (player.slow.fillAmount > 0)
        {
            StartCoroutine(UnfillImage(player));
        }
        else
        {
            player.isSlowed = false;
        }
    }
}
