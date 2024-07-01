
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : Singleton<EffectController>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //DECIDE TO OBJECT POOL OR NOT
    public void PlayEffect(GameObject fx, Transform targetPos)
    {
        ParticleSystem gameObject = Instantiate(fx, targetPos.position, Quaternion.identity).GetComponent<ParticleSystem>();
    }
}
