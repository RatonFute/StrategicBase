using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public void SpawnEntity(GameObject prefab)
    {
        GameObject objectPool = ObjectPoolManager.Instance.GetObjectFromPool(prefab);
        if (objectPool.GetComponent<TacticCenter>())
        {
            objectPool.transform.position = GameSceneUIManager.Instance.CurrentTacticCenter.transform.position;
        }
    }
}
