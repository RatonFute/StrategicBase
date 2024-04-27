using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRange : MonoBehaviour
{
    public List<GameObject> TargetsInRange { get; } = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        TargetsInRange.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        TargetsInRange.Remove(other.gameObject);
    }

    private void RemoveEnemyFromList(GameObject enemy)
    {
        TargetsInRange.Remove(enemy);
    }

    private void OnEnable()
    {
        Enemy.OnDied += RemoveEnemyFromList;
    }

    private void OnDisable()
    {
        Enemy.OnDied -= RemoveEnemyFromList;
    }
}
