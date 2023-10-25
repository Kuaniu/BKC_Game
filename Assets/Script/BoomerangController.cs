using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    //绑定刚体
    private Rigidbody2D BoomerangRb;
    //武器攻击伤害
    public float BoomerangDamage;
    //自转速度
    public float rotation;
    //移动速度
    public float MoveSpeed;
    //是否返回
    public bool isReturn;
    //角色
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
        //武器自转
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