using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManage : MonoBehaviour
{
    //���������˺�
    public float FireBallDamage;
    public float MoveSpeed;
    private Transform Player;
    void Start()
    {
        transform.localPosition = new Vector2(10, 0);//��ʼ��λ��
        Player = GameObject.Find("Player").GetComponent<Transform>();//�󶨽�ɫ
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.forward, -MoveSpeed * Time.deltaTime * 10);
    }
}
