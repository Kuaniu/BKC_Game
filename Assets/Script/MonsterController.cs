using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform player;
    private bool contact;
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        contact = false;
        GameObject.Find("GameController").GetComponent<GameController>().MonsterListAdd(gameObject);
        monsterRenderer = GetComponent<SpriteRenderer>();
        MonsterHP = 3;
    }
    void Update()
    {

        //若未接触角色则朝着玩家位置前进
        if(!contact)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("角色与怪物发生碰撞");
            contact = true;
        }
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
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }
    //怪物受伤闪烁
    public IEnumerator monsterShake()
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
