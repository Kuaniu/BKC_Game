using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform player;
    private bool contact;

    //基本参数
    [Header("移动速度")]
    public float monsterMoveSpeed;
    [Header("生命值")]
    public float MonsterHP;
    //[Header("攻击力")]
    //public float Damage;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        contact = false;
        GameObject.Find("GameController").GetComponent<GameController>().MonsterListAdd(gameObject);
        MonsterHP = 3;
    }
    void Update()
    {
        //若未接触角色则朝着玩家位置前进
        if(!contact)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = true;
        }
        else
        {
            //控制怪物之间的距离
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
        else
        {
            //控制怪物之间的距离
        }
    }
    //public float GetDamage()
    //{
    //    return Damage;
    //}

}
