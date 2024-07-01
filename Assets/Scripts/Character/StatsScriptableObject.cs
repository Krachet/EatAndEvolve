using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class StatsScriptableObject : ScriptableObject
{
    public int level;
    public int damage;
    public int foodPerDrop;
}
[CreateAssetMenu(fileName = "BreakableObjectStats", menuName = "ScriptableObjects/BreakableObject", order = 2)]
public class BreakableObjectStats : StatsScriptableObject
{
    public float health;
    public int foodDrop;
}
