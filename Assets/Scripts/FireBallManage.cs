using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManage : MonoBehaviour
{
    //���������˺�
    public float FireBallDamage;
    private Transform Player;
    private float MoveSpeed;
    void Start()
    {
        transform.localPosition = new Vector2(10, 0);//��ʼ��λ��
        Player = GameObject.Find("Player").GetComponent<Transform>();//�󶨽�ɫ
        MoveSpeed = 20;//���ó�ʼ�˶��ٶ�
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.forward, -MoveSpeed * Time.deltaTime * 10);
    }
}
