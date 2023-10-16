using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;//HP�ĵ�����Ҫ���õ�UIԴ�ļ�
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class PlayerController : MonoBehaviour
{
    [Header("�ƶ��ٶ�")]
    public float PlayerMoveSpeed;
    [Header("����ֵ")]
    public Slider PlayerHP;
    //[Header("������")]
    //public float Damage;

    private float Experience;

    void Start()
    {
        PlayerHP.value = 1;
        Experience = 0;
    }
    //�����˶�����
    private void FixedUpdate()
    {
        playerMove();
    }
    void Update()
    {
        playerHp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����ɫ�����������Ѫ
        if (collision.gameObject.CompareTag("Monster"))
        {
            PlayerHP.value -= 0.1f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//�������Ӵ����
    {
        //���������ɫ����<=1.5���������ɫ����
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 1.5f)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, 5 * Time.deltaTime);
        }
        //ʰȡ������
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 0.6f)
        {
            Experience += collision.gameObject.GetComponent<Experience>().xp;
            Destroy(collision.gameObject);
        }
    }
    void playerMove()//��ɫ�ƶ� 

    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H, V) * PlayerMoveSpeed * Time.deltaTime);
    }
    void playerHp()//��ɫ����ֵ

    {
        //����ֵ
        if (PlayerHP.value == 0)
        {
            //��Ϸ��ͣ
            PlayerHP.gameObject.SetActive(false);//����Ѫ��UI
            Time.timeScale = 0;
            //���������������л�����/�����˵�
        }
        //����Ѫ���仯
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerHP.value -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHP.value += 0.1f;
        }
    }
}
