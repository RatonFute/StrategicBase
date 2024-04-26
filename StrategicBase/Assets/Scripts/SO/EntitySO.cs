using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitySO", menuName = "Create SO/EntitySO")]
public class EntitySO : ScriptableObject
{
    public float HealthPoint;
    public float Attack;
    public float Speed;
    public float AttackRange;
}
