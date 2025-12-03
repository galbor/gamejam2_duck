using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A pool of non-active objects for reuse.
 */
public class ObjectPoolManager : MonoBehaviour
{
    private static class Constants
    {
        internal const int DefaultPoolSize = 10;
    }

    private List<GameObject> _pool;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _poolParent;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (!_prefab)
        {
            Debug.LogError("Prefab is missing.");
        }
        _pool = new List<GameObject>();
    }

    /**
     * Initialize a pool of non-active objects.
     */
    internal void InitPool(GameObject prefab, int poolSize = Constants.DefaultPoolSize)
    {
        if (!prefab)
        {
            Debug.LogError("Prefab is missing.");
        }
        else if (poolSize <= 0)
        {
            Debug.LogError("Pool size is invalid(Probably non-positive).");
        }
        
        _prefab = prefab;
        for (int i = 0; i < poolSize; i++)
        {
            expandPool().SetActive(false);
        }
    }

    /**
     * Expands the pool by one game object,
     * and then gets the new object from the pool.
     */
    private GameObject expandPool()
    {
        if (!_prefab)
        {
            Debug.LogError("Couldn't expand pool - Prefab is missing.");
            return null;
        }
        GameObject obj = Instantiate(_prefab, _poolParent);
        _pool.Add(obj);
        obj.name = nameof(_prefab) + " " + GetPoolSize();
        return obj;
    }

    /**
     * Gets the current amount of objects in the pool.
     */
    private int GetPoolSize()
    {
        return _pool.Count;
    }

    /**
     * Gets an object from the pool.
     */
    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        
        return expandPool();
    }
    
    /**
     * Returns an object to the pool.
     */
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
