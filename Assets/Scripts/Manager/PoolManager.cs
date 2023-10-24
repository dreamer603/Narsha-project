using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    private List<GameObject>[] _pools;

    private void Awake()
    {
        _pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < _pools.Length; index++)
        {
            _pools[index] = new List<GameObject>();
        }
    }
    
    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in _pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            _pools[index].Add(select);
        }

        return select;
    }
}
