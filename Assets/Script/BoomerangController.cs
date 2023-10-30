using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    //�󶨸���
    private Rigidbody2D BoomerangRb;
    //���������˺�
    public float BoomerangDamage;
    //��ת�ٶ�
    public float rotation;
    //�ƶ��ٶ�
    public float MoveSpeed;
    //�Ƿ񷵻�
    public bool isReturn;
    //�Ƿ��һ�νӴ���ɫ
    public bool leave;
    //��ɫ
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
        //������ת
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

    private void TimeDestroyGameobj()    //ʱ�����ɾ������
    {        
            Destroy(gameObject);
    }
}