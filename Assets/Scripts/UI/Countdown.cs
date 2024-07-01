using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class Countdown : MonoBehaviour
{
    [SerializeField] GameObject timerFill;
    private float timer;
    private void Start()
    {
        timer = 5;
    }

    private void Update()
    {
        
    }

    public IEnumerator AnimateTimer()
    {
        yield return new WaitForSeconds(0.5f);
        LeanTween.scaleX(timerFill, 0, timer);
        yield return new WaitForSeconds(timer + 0.5f);
        StartCoroutine(GameController.Instance.AfterBossFight());
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }
}
