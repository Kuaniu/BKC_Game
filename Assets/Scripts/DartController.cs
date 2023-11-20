using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DartController : MonoBehaviour
{
    //武器攻击伤害
    public float DartDamage;
    //移动速度
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
        //面朝怪物
        // 计算物体应该旋转到面朝目标物体的角度
        Vector3 targetDirection = pos.transform.position - gameObject.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // 设置物体的rotation，只改变z值
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }
    private void FixedUpdate()
    {
        if (pos == null)
        {
            return;
        }
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
