using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DartController : MonoBehaviour
{
    //���������˺�
    public float DartDamage;
    //�ƶ��ٶ�
    public float MoveSpeed;

    private GameObject pos;
    private void Start()
    {
        pos = GameObject.Find("GameController").GetComponent<GameController>().FindClosestMonster();
        if(pos==null)
        {
            Destroy(gameObject);
            return;
        }

        InvokeRepeating("TimeDestroyGameobj",0.5f, 1);

    }
    private void Update()
    {
        if (pos == null)
        {
            Destroy(gameObject);
            return;
        }
        //�泯����
        // ��������Ӧ����ת���泯Ŀ������ĽǶ�
        Vector3 targetDirection = pos.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // ���������rotation��ֻ�ı�zֵ
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }
    private void FixedUpdate()
    {
        if (pos == null)
        {
            return;
        }
        //�����ƶ�
        transform.position = Vector3.MoveTowards(transform.position, pos.transform.position, MoveSpeed * Time.deltaTime);

    }
    private void TimeDestroyGameobj()    //ʱ�����ɾ������
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
