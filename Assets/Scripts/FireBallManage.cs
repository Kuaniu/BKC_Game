using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManage : MonoBehaviour
{
    private Transform Player;
    public static float FireBallDamage=5;
    private static float MoveSpeed=20;
    private static Vector3 FlagNum;

    void Start()
    {
        transform.localPosition = new Vector2(10, 0);//��ʼ��λ��
        Player = GameObject.Find("Player").GetComponent<Transform>();//�󶨽�ɫ
        FlagNum = transform.localScale;
    }
    void Update()
    {
        transform.localScale = FlagNum;
        transform.RotateAround(Player.position, Vector3.forward, -MoveSpeed * Time.deltaTime * 10);
    }
    public static void SetMoveSpeed()
    {
        MoveSpeed *=2;
    }
    public static void SetDamage()
    {
        FireBallDamage *=2;
    }
    public static void SetScale()
    {
        FlagNum *= 2;
    }

}
