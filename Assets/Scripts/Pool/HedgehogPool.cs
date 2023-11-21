using System.Collections.Generic;
using UnityEngine;

public class HedgehogPool : MonoBehaviour
{
    public GameObject hedgehogPrefab; // ��⬵�Ԥ����
    public int poolSize = 10; // ����ش�С

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

        // ���û�п��õĴ�⬣�����ѡ��̬����һ���µĴ�⬲���ӵ�����
        GameObject newHedgehog = Instantiate(hedgehogPrefab);

        hedgehogPool.Add(newHedgehog);
        return newHedgehog;
    }

    public void ReturnHedgehogToPool(GameObject hedgehog)
    {
        hedgehog.SetActive(false);
    }
}
