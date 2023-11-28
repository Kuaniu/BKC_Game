using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float xp;
    public string ExpName;
    private ExpBallPool expballPool;//经验球对象池
    private Transform Player;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
        expballPool = GameObject.Find("GameController").GetComponent<ExpBallPool>();
    }
    private void Update()
    {
        DistanceTracing();
    }
    public void DistanceTracing()//经验球距离检测
    {
        if (Vector2.Distance(Player.position, transform.position) >= 15)
        {
            ReturnExpBallPool();
        }
    }
    public void ReturnExpBallPool()//回收
    {
        expballPool.ReturnObjectToPool(ExpName, gameObject);
    }
}
