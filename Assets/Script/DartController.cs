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

    private Vector3 pos;
    private void Start()
    {
        var obj = GameObject.Find("GameController");
        if(obj==null)
        {
            return;
        }
        pos = obj.GetComponent<GameController>().FindClosestMonster();
        if(pos==Vector3.zero)
        {
            Destroy(gameObject);
        }

        InvokeRepeating("TimeDestroyGameobj",0.5f, 1);

    }
    private void Update()
    {
        //�泯����
        // ��������Ӧ����ת���泯Ŀ������ĽǶ�
        Vector3 targetDirection = pos - gameObject.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // ���������rotation��ֻ�ı�zֵ
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }
    private void FixedUpdate()
    {
        //�����ƶ�
        transform.position = Vector3.MoveTowards(transform.position, pos, MoveSpeed * Time.deltaTime);

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
