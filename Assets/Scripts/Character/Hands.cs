using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hands : MonoBehaviour
{

    public Character GetParent()
    {
        return transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<Character>();
    }
}
