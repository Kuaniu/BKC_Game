using System.Collections.Generic;
using UnityEngine;

public class BirdPool : MonoBehaviour
{
    public GameObject birdPrefab; // ���Ԥ����
    public int poolSize = 10; // ����ش�С

    private List<GameObject> birdPool = new List<GameObject>();

    void Start()
    {
        InitializeBirdPool();
    }

    void InitializeBirdPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bird = Instantiate(birdPrefab);
            bird.SetActive(false);
            birdPool.Add(bird);
        }
    }

    public GameObject GetBirdFromPool()
    {
        foreach (GameObject bird in birdPool)
        {
            if (!bird.activeInHierarchy)
            {
                bird.SetActive(true);
                return bird;
            }
        }

        // ���û�п��õ��񣬿���ѡ��̬����һ���µ�����ӵ�����
        GameObject newBird = Instantiate(birdPrefab);

        birdPool.Add(newBird);
        return newBird;
    }

    public void ReturnBirdToPool(GameObject bird)
    {
        bird.SetActive(false);
    }
}
