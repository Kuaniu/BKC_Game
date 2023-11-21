using System.Collections.Generic;
using UnityEngine;

public class ScarecrowPool : MonoBehaviour
{
    public GameObject scarecrowPrefab; // 稻草人的预制体
    public int poolSize = 10; // 对象池大小

    private List<GameObject> scarecrowPool = new List<GameObject>();

    void Start()
    {
        InitializeScarecrowPool();
    }

    void InitializeScarecrowPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject scarecrow = Instantiate(scarecrowPrefab);
            scarecrow.SetActive(false);
            scarecrowPool.Add(scarecrow);
        }
    }

    public GameObject GetScarecrowFromPool()
    {
        foreach (GameObject scarecrow in scarecrowPool)
        {
            if (!scarecrow.activeInHierarchy)
            {
                scarecrow.SetActive(true);
                return scarecrow;
            }
        }

        // 如果没有可用的稻草人，可以选择动态创建一个新的稻草人并添加到池中
        GameObject newScarecrow = Instantiate(scarecrowPrefab);

        scarecrowPool.Add(newScarecrow);
        return newScarecrow;
    }

    public void ReturnScarecrowToPool(GameObject scarecrow)
    {
        scarecrow.SetActive(false);
    }
}
