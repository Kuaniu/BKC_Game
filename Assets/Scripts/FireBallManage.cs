using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManage : MonoBehaviour
{
    //武器攻击伤害
    public float FireBallDamage;
    private Transform Player;
    private float MoveSpeed;
    void Start()
    {
        transform.localPosition = new Vector2(10, 0);//初始化位置
        Player = GameObject.Find("Player").GetComponent<Transform>();//绑定角色
        MoveSpeed = 20;//设置初始运动速度
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Player.position, Vector3.forward, -MoveSpeed * Time.deltaTime * 10);
    }
}
