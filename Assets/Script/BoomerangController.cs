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
    //��ɫ
    public Transform player;
    private void Start()
    {
        BoomerangRb = GetComponent<Rigidbody2D>();
        isReturn = true;
        StartCoroutine("SetReturn");
        InvokeRepeating("DestroyGameobj", 3, 1);

        player=GameObject.Find("Player").GetComponent<Transform>();

    }
    private void FixedUpdate()
    {
        //������ת
        transform.Rotate(Vector3.forward, rotation);
        var obj = GameObject.Find("GameController").GetComponent<GameController>().FindClosestMonster();

        if (obj != Vector3.zero)
        {
            Vector2 pos = (obj - transform.position).normalized;
            if (!isReturn)
            {
                BoomerangRb.velocity += pos * MoveSpeed * Time.deltaTime;
            }
            else
            {
                pos = (player.position - transform.position).normalized;
                BoomerangRb.velocity += pos * MoveSpeed * Time.deltaTime;
            }
        }


    }
    private void DestroyGameobj()
    {
        Destroy(gameObject); 
    }

    private IEnumerator SetReturn()
    {
        while (true)
        {
            isReturn = !isReturn;
            yield return new WaitForSeconds(2f);
        }
    }
}