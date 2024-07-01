using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthbarUI : Singleton<HealthbarUI>
{
    public GameObject getCanvas()
    {
        return gameObject;
    }
}
