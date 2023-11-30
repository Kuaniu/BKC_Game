using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    private Transform player;
    private GameObject Thepos;
    private Rigidbody2D BoomerangRb;

    private float rotation;
    private bool isReturn;
    private bool leave;

    public static float BoomerangDamage;
    public static float MoveSpeed;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        Thepos = GameObject.Find("GameController").GetComponent<MonsterPools>().GetFarthestMonster(player.position);
        BoomerangRb = GetComponent<Rigidbody2D>();

        rotation = 20;
        isReturn = false;
        leave = false;

        BoomerangDamage = 5;
        MoveSpeed = 1;

        InvokeRepeating("TimeDestroyGameobj", 5, 1);
    }
    private void FixedUpdate()
    {
        //武器自转
        transform.Rotate(Vector3.forward, rotation);

        if (Thepos && Thepos.transform.position != Vector3.zero)
        {
            Vector2 pos = (Thepos.transform.position - transform.position).normalized;
            if (!isReturn)
            {
                BoomerangRb.velocity += pos * MoveSpeed;
            }
            else
            {
                pos = (player.position - transform.position).normalized;
                BoomerangRb.velocity = pos * MoveSpeed * 15;
            }
        }
    }
    public static void BoomerangDamageDouble()
    {
        BoomerangDamage *= 2;
    }
    public static void MoveSpeedUp()
    {
        MoveSpeed += 1;
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