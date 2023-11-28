using System.Collections;
using Unity.VisualScripting;
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
    public float monsterMoveSpeed;
    public float MonsterHP;
    public string monsterName;
    public float Damage;
    public bool isStop;

    private float RecordHP;
    private MonsterPools monsterPool;//怪物对象池
    private ExpBallPool expballPool;//经验球对象池

    void Start()
    {
        RecordHP = MonsterHP;
        monsterPool = GameObject.Find("GameController").GetComponent<MonsterPools>();
        expballPool = GameObject.Find("GameController").GetComponent<ExpBallPool>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            MonsterHP -= BoomerangController.BoomerangDamage;
        }

        //碰到武器Dart检测
        if (collision.gameObject.CompareTag("Dart"))
        {
            MonsterHP -= DartController.DartDamage;
        }
        //碰到武器FireBall检测
        if (collision.gameObject.CompareTag("FireBall"))
        {
            MonsterHP -= collision.GetComponent<FireBallManage>().FireBallDamage;
        }
        DestroyManage();

    }
    public void DestroyManage()//怪物血量小于等于0则删除
    {
        if (MonsterHP <= 0)
        {
            MonsterHP = RecordHP;
            ReturnMonster(monsterName);
            //获取游戏时间，时间越久最高经验球掉落概率越大
            //掉落经验xpPrefab
            SpawnExp();
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
            ReturnMonster(monsterName);
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
    private void SpawnExp()//经验球对象池
    {
        int Rnum=Random.Range(0, 100);//1-7.8-9.10
        GameObject ExpObj;
        if (Rnum<80)
        {
            ExpObj = expballPool.GetObjectFromPool("Exp4");
        }
        else if(Rnum<90)
        {
            ExpObj = expballPool.GetObjectFromPool("Exp3");
        }
        else if (Rnum < 98)
        {
            ExpObj = expballPool.GetObjectFromPool("Exp2");
        }
        else
        {
            ExpObj = expballPool.GetObjectFromPool("Exp1");
        }
        ExpObj.transform.position = transform.position;
    }
    private void ReturnMonster(string monsterName)//回收
    {
        monsterPool.ReturnObjectToPool(monsterName, gameObject);
    }
}