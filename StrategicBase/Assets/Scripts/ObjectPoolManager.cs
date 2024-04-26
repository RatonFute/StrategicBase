using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject prefab;
    public int poolSize;
}

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Transform objectPoolContainer;
    [SerializeField] private List<ObjectPoolItem> _poolItems;
    private Dictionary<GameObject, List<GameObject>> objectPoolDictionary = new Dictionary<GameObject, List<GameObject>>();

    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        foreach (ObjectPoolItem item in _poolItems)
        {
            List<GameObject> objectPool = new List<GameObject>();
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject obj = Instantiate(item.prefab, objectPoolContainer);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            objectPoolDictionary.Add(item.prefab, objectPool);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab)
    {
        if (objectPoolDictionary.ContainsKey(prefab))
        {
            List<GameObject> objectPool = objectPoolDictionary[prefab];
            foreach (GameObject obj in objectPool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // Si tous les objets sont actifs, en créer un nouveau
            GameObject newObj = Instantiate(prefab, objectPoolContainer);
            objectPool.Add(newObj);
            return newObj;
        }
        else
        {
            Debug.LogError("Object pool for prefab " + prefab.name + " not found!");
            return null;
        }
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}

