using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerEffect : MonoBehaviour
{
    public Color color;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnTriggerDown()
    {
        image.color = color;
    }

    public void OnTriggerUp()
    {
        image.color = Color.white;
    }
}
