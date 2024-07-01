using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] Character parent;

    private void Start()
    {
        parent = transform.parent.gameObject.GetComponent<Character>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BreakableObjects")
        {
            parent.targets.Add(other.gameObject);
            parent.target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BreakableObjects")
        {
            parent.targets.Remove(other.gameObject);
            parent.target = null; 
        }    
    }
}
