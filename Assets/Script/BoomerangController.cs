using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    //绑定刚体
    private Rigidbody2D BoomerangRb;
    //武器攻击伤害
    public float BoomerangDamage;
    //自转速度
    public float rotation;
    //移动速度
    public float MoveSpeed;
    //是否返回
    public bool isReturn;
    //是否第一次接触角色
    public bool leave;
    //角色
    private Transform player;
    private void Start()
    {
        BoomerangRb = GetComponent<Rigidbody2D>();
        isReturn = false;
        player = GameObject.Find("Player").GetComponent<Transform>();
        leave = false;

        InvokeRepeating("TimeDestroyGameobj", 5, 1);
    }
    private void FixedUpdate()
    {
        //武器自转
        transform.Rotate(Vector3.forward, rotation);
        var obj = GameObject.Find("GameController").GetComponent<GameController>();
        if(obj==null)
        {
            return;
        }
        if (obj.FindClosestMonster() != Vector3.zero)
        {
            Vector2 pos = (obj.FindClosestMonster() - transform.position).normalized;
            if (!isReturn)
            {
                BoomerangRb.velocity += pos * MoveSpeed;
            }
            else
            {
                pos = (player.position - transform.position).normalized;
                BoomerangRb.velocity = pos * MoveSpeed*10;
            }
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            leave = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster")&& leave)
        {
            isReturn = true;
        }
        if (collision.gameObject.CompareTag("Player") && leave)
        {
            Destroy(gameObject);
        }
    }

    private void TimeDestroyGameobj()    //时间过长删除自身
    {        
            Destroy(gameObject);
    }
}