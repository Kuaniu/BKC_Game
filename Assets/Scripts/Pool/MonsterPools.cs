using System.Collections.Generic;
using UnityEngine;

public class MonsterPools : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, List<GameObject>> poolDictionary;

    void Start()
    {
        InitializePools();
    }

    void InitializePools()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            List<GameObject> monsterPool = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                monsterPool.Add(obj);
            }

            poolDictionary.Add(pool.tag, monsterPool);
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            foreach (GameObject obj in poolDictionary[tag])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            // 如果没有可用的对象，可以选择动态创建一个新的对象并添加到池中
            Pool pool = GetPoolWithTag(tag);
            if (pool != null)
            {
                GameObject newObj = Instantiate(pool.prefab);
                poolDictionary[tag].Add(newObj);
                return newObj;
            }
        }

        return null;
    }

    public void ReturnObjectToPool(string tag, GameObject obj)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            obj.SetActive(false);
        }
    }

    private Pool GetPoolWithTag(string tag)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
            {
                return pool;
            }
        }
        return null;
    }

    public GameObject GetClosestMonster(Vector3 playerPosition)
    {
        GameObject closestMonster = null;
        float closestDistance = float.MaxValue;

        foreach (Pool pool in pools)
        {
            foreach (GameObject monster in poolDictionary[pool.tag])
            {
                if (monster.activeInHierarchy)
                {
                    float distance = Vector3.Distance(playerPosition, monster.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMonster = monster;
                    }
                }
            }
        }

        return closestMonster;
    }

    public GameObject GetFarthestMonster(Vector3 playerPosition)
    {
        GameObject farthestMonster = null;
        float farthestDistance = 0f;

        foreach (Pool pool in pools)
        {
            foreach (GameObject monster in poolDictionary[pool.tag])
            {
                if (monster.activeInHierarchy)
                {
                    float distance = Vector3.Distance(playerPosition, monster.transform.position);

                    if (distance > farthestDistance)
                    {
                        farthestDistance = distance;
                        farthestMonster = monster;
                    }
                }
            }
        }

        return farthestMonster;
    }
}
