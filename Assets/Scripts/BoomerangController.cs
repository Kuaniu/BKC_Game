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

    private GameObject Thepos;
    private void Start()
    {
        BoomerangRb = GetComponent<Rigidbody2D>();
        isReturn = false;
        player = GameObject.Find("Player").GetComponent<Transform>();
        leave = false;

        InvokeRepeating("TimeDestroyGameobj", 5, 1);
        Thepos = GameObject.Find("GameController").GetComponent<GameController>().FindClosestMonster();
        if(Thepos==null)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void FixedUpdate()
    {
        if (Thepos == null)
        {
            Destroy(gameObject);
            return;
        }
        //武器自转
        transform.Rotate(Vector3.forward, rotation);

        if (Thepos.transform.position != Vector3.zero)
        {
            Vector2 pos = (Thepos.transform.position - transform.position).normalized;
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
        if(Thepos==null)
        {
            isReturn = true;
        }
    }
    private void TimeDestroyGameobj()    //时间过长删除自身
    {        
            Destroy(gameObject);
    }
}