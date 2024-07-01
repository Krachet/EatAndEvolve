using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    public GameObject hand;

    public void SetActiveHand(bool value)
    {
        hand.SetActive(value);
    }
}
