using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform player;
    private bool contact;

    //��������
    [Header("�ƶ��ٶ�")]
    public float monsterMoveSpeed;
    [Header("����ֵ")]
    public float MonsterHP;
    //[Header("������")]
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

        //��δ�Ӵ���ɫ�������λ��ǰ��
        if(!contact)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("��ɫ����﷢����ײ");
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
