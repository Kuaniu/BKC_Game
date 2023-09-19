using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
            print("角色与怪物发生碰撞");
            contact = true;
        }
        if (collision.gameObject.CompareTag("Bull"))
        {
            Destroy(gameObject);

        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }
    
}
