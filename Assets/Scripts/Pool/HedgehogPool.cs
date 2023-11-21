using System.Collections.Generic;
using UnityEngine;

public class HedgehogPool : MonoBehaviour
{
    public GameObject hedgehogPrefab; // 刺猬的预制体
    public int poolSize = 10; // 对象池大小

    private List<GameObject> hedgehogPool = new List<GameObject>();

    void Start()
    {
        InitializeHedgehogPool();
    }

    void InitializeHedgehogPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject hedgehog = Instantiate(hedgehogPrefab);
            hedgehog.SetActive(false);
            hedgehogPool.Add(hedgehog);
        }
    }

    public GameObject GetHedgehogFromPool()
    {
        foreach (GameObject hedgehog in hedgehogPool)
        {
            if (!hedgehog.activeInHierarchy)
            {
                hedgehog.SetActive(true);
                return hedgehog;
            }
        }

        // 如果没有可用的刺猬，可以选择动态创建一个新的刺猬并添加到池中
        GameObject newHedgehog = Instantiate(hedgehogPrefab);

        hedgehogPool.Add(newHedgehog);
        return newHedgehog;
    }

    public void ReturnHedgehogToPool(GameObject hedgehog)
    {
        hedgehog.SetActive(false);
    }
}
