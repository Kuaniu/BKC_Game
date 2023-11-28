using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DartController : MonoBehaviour
{
    public static int DartLevel=1;
    public static float DartDamage=2.5f;
    public static float MoveSpeed=10;

    private Transform player;
    private GameObject pos;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        pos = GameObject.Find("GameController").GetComponent<MonsterPools>().GetClosestMonster(player.position);

        InvokeRepeating("TimeDestroyGameobj",0.5f, 1);
        SetDartRotation();
    }
    private void SetDartRotation()
    {
        //面朝怪物
        //计算物体应该旋转到面朝目标物体的角度
        Vector3 targetDirection = pos.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        //设置物体的rotation，只改变z值
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
    public static void DamageDouble()
    {
        DartDamage *= 2;
    }
    public static void MoveSpeedUp()
    {
        MoveSpeed += 5;
    }
    private void FixedUpdate()
    {
        //武器移动
        transform.position = Vector3.MoveTowards(transform.position, pos.transform.position, MoveSpeed * Time.deltaTime);

    }
    private void TimeDestroyGameobj()    //时间过长删除自身
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
