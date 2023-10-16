using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;//HP的调用需要引用的UI源文件
//using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;


public class PlayerController : MonoBehaviour
{
    [Header("移动速度")]
    public float PlayerMoveSpeed;
    [Header("生命值")]
    public Slider PlayerHP;
    //[Header("攻击力")]
    //public float Damage;

    private float Experience;

    void Start()
    {
        PlayerHP.value = 1;
        Experience = 0;
    }
    //物理运动更新
    private void FixedUpdate()
    {
        playerMove();
    }
    void Update()
    {
        playerHp();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //若角色碰到怪物则扣血
        if (collision.gameObject.CompareTag("Monster"))
        {
            PlayerHP.value -= 0.1f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//触发器接触检测
    {
        //经验球与角色距离<=1.5则经验球向角色飞行
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 1.5f)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, 5 * Time.deltaTime);
        }
        //拾取经验球
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 0.6f)
        {
            Experience += collision.gameObject.GetComponent<Experience>().xp;
            Destroy(collision.gameObject);
        }
    }
    void playerMove()//角色移动 

    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H, V) * PlayerMoveSpeed * Time.deltaTime);
    }
    void playerHp()//角色生命值

    {
        //生命值
        if (PlayerHP.value == 0)
        {
            //游戏暂停
            PlayerHP.gameObject.SetActive(false);//隐藏血条UI
            Time.timeScale = 0;
            //播放死亡动画，切换场景/弹出菜单
        }
        //测试血条变化
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerHP.value -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHP.value += 0.1f;
        }
    }
}
