using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAnimation : MonoBehaviour
{
    public GameObject levelUpAnimation;

    public void LevelUpAnimation()
    {
        EffectController.Instance.PlayEffect(levelUpAnimation, transform);
    }
}
