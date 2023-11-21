using System.Collections.Generic;
using UnityEngine;

public class BirdPool : MonoBehaviour
{
    public GameObject birdPrefab; // 鸟的预制体
    public int poolSize = 10; // 对象池大小

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

        // 如果没有可用的鸟，可以选择动态创建一个新的鸟并添加到池中
        GameObject newBird = Instantiate(birdPrefab);

        birdPool.Add(newBird);
        return newBird;
    }

    public void ReturnBirdToPool(GameObject bird)
    {
        bird.SetActive(false);
    }
}
