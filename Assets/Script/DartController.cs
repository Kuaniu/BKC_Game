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
    private void FixedUpdate()
    {
        //�����ƶ�
        var obj = GameObject.Find("GameController").GetComponent<GameController>();
        if (obj == null)
        {
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, obj.FindClosestMonster(), MoveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
