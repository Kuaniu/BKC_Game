using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //获取角色transform
    private Transform player;
 
    //怪物是否接触到角色
    private bool contact = false;
    //获取怪物Renderer
    SpriteRenderer monsterRenderer;

    //基本参数
    [Header("移动速度")]
    public float monsterMoveSpeed;
    [Header("生命值")]
    public float MonsterHP;
    [Header("Experience")]
    public GameObject xpPrefab;
    //[Header("攻击力")]
    //public float Damage;
    void Start()
    {
        //获取角色的transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject.Find("GameController").GetComponent<GameController>().MonsterListAdd(gameObject);
        //获取怪物Renderer
        monsterRenderer = GetComponent<SpriteRenderer>();
        //怪物初始血量为3
        MonsterHP = 3;
    }
    private void FixedUpdate()//物理运动
    {
        //怪物向量追踪
        if (!contact)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }
    }
    void Update()
    {

        //修改怪物面朝方向
        if(gameObject.transform.position.x>player.position.x)
        {
            monsterRenderer.flipX = true;
        }
        else
        {
            monsterRenderer.flipX = false;
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到角色检测
        if (collision.gameObject.CompareTag("Player"))
        {
            print("角色与怪物发生碰撞");
            contact = true;
        }
        //碰到武器Bull检测
        if (collision.gameObject.CompareTag("Bull"))
        {
            MonsterHP -= 2;
            StartCoroutine(monsterShake());
            if(MonsterHP<=0)
            {
                Destroy(gameObject);

                //获取游戏时间，时间越久最高经验球掉落概率越大
                Quaternion quaternion = Quaternion.identity;            
                //掉落经验xpPrefab
                Instantiate(xpPrefab,gameObject.transform.position,quaternion);
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //离开角色检测
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }
    
    public IEnumerator monsterShake()//怪物受伤闪烁
    {
        // 定义抖动效果的持续时间
        float shakeDuration = 0.2f;

        // 定义闪烁效果的持续时间
        float blinkDuration = 0.1f;

        // 定义闪烁效果之间的间隔
        float blinkInterval = 0.05f;

        // 在抖动效果的持续时间内，在起始位置和随机位置之间进行插值
        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            // 闪烁角色
            if (t % blinkInterval < blinkDuration)
            {
                monsterRenderer.enabled = !monsterRenderer.enabled;
            }
            else
            {
                monsterRenderer.enabled = true;
            }
            yield return null;
        }

        // 确保Sprite可见
        monsterRenderer.enabled = true;
    }
}
