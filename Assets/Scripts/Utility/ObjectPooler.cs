using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private static ObjectPooler _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }
    public static ObjectPooler Instance()
    {
        return _instance;
    }

    public GameObject objectToPool;
    public int amountToPool;
    public bool canExpand;

    private List<GameObject> pool;

    private void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject go=Instantiate(objectToPool);
            go.SetActive(false);
            pool.Add(go);
        }
    }

    public GameObject getPooledObject()
    {
        //just an available pooled object
        foreach(GameObject go in pool)
        {
            if (go.activeInHierarchy == false)
            {
                return go;
            }
        }

        //no pooled objects, should we create more?
        if (canExpand)
        {
            GameObject go = Instantiate(objectToPool);
            go.SetActive(false);
            pool.Add(go);
            return go;
        }


        return null;
    }

}
