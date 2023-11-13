using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //获取角色transform
    private Transform player;

    //怪物是否接触到角色
    private bool contact = false;
    //获取怪物Renderer
    SpriteRenderer monsterRenderer;
    //更新怪物是否可以移动
    private bool isMove = true;

    //基本参数
    [Header("移动速度")]
    public float monsterMoveSpeed;
    [Header("怪物生命值")]
    public float MonsterHP;
    [Header("Experience")]
    public GameObject xpPrefab;
    [Header("攻击力")]
    public float Damage;
    [Header("是否时而停止")]
    public bool isStop;

    void Start()
    {
        //获取角色的transform
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //把本身加入GameController中的怪物列表里
        GameObject.Find("GameController").GetComponent<GameController>().MonsterListAdd(transform);

        //获取怪物Renderer
        monsterRenderer = GetComponent<SpriteRenderer>();

        //持续切换怪物是否可以移动
        if (isStop)
        {
            StartCoroutine("IsMoveUpdate");
        }
    }
    private void FixedUpdate()//物理运动
    {
        //怪物向量追踪
        VectorTracing();
        //怪物距离检测
        DistanceTracing();
    }
    void Update()
    {
        //修改怪物面朝方向
        if (gameObject.transform.position.x > player.position.x)
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
            contact = true;
        }

        //碰到武器Boomerang检测
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            MonsterHP -= collision.GetComponent<BoomerangController>().BoomerangDamage;
            StartCoroutine(monsterShake());
            DestroyManage();
        }

        //碰到武器Dart检测
        if (collision.gameObject.CompareTag("Dart"))
        {
            MonsterHP -= collision.GetComponent<DartController>().DartDamage;
            StartCoroutine(monsterShake());
            DestroyManage();
        }
        //碰到武器FireBall检测
        if (collision.gameObject.CompareTag("FireBall"))
        {
            MonsterHP -= collision.GetComponent<FireBallManage>().FireBallDamage;
            StartCoroutine(monsterShake());
            DestroyManage();
        }
    }
    public void DestroyManage()//怪物血量小于等于0则删除
    {
        if (MonsterHP <= 0)
        {
            //出列
            GameObject.Find("GameController").GetComponent<GameController>().listTemp.Remove(transform);
            Destroy(gameObject);
            //获取游戏时间，时间越久最高经验球掉落概率越大
            //掉落经验xpPrefab
            Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
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

    public void VectorTracing()//怪物向量追踪
    {
        if (!contact && isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }
    }
    public void DistanceTracing()//怪物距离检测
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 15)
        {
            GameObject.Find("GameController").GetComponent<GameController>().listTemp.Remove(transform);
            Destroy(gameObject);
        }
    }
    public IEnumerator IsMoveUpdate()//更新怪物是否移动
    {
        while (true)
        {
            if (isMove)
            {
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }
            isMove = !isMove;
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