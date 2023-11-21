using System.Collections.Generic;
using UnityEngine;

public class ScarecrowPool : MonoBehaviour
{
    public GameObject scarecrowPrefab; // �����˵�Ԥ����
    public int poolSize = 10; // ����ش�С

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

        // ���û�п��õĵ����ˣ�����ѡ��̬����һ���µĵ����˲���ӵ�����
        GameObject newScarecrow = Instantiate(scarecrowPrefab);

        scarecrowPool.Add(newScarecrow);
        return newScarecrow;
    }

    public void ReturnScarecrowToPool(GameObject scarecrow)
    {
        scarecrow.SetActive(false);
    }
}
