using System.Collections;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    //��ȡ��ɫtransform
    private Transform player;

    //�����Ƿ�Ӵ�����ɫ
    private bool contact = false;
    //��ȡ����Renderer
    SpriteRenderer monsterRenderer;
    //���¹����Ƿ�����ƶ�
    private bool isMove = true;

    //��������
    public float monsterMoveSpeed;
    public float MonsterHP;
    public string monsterName;
    public float Damage;
    public bool isStop;

    private float RecordHP;
    private MonsterPools monsterPool;//��������
    private ExpBallPool expballPool;//����������

    void Start()
    {
        RecordHP = MonsterHP;
        monsterPool = GameObject.Find("GameController").GetComponent<MonsterPools>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monsterRenderer = GetComponent<SpriteRenderer>();

        //�����л������Ƿ�����ƶ�
        if (isStop)
        {
            StartCoroutine("IsMoveUpdate");
        }
    }
    private void FixedUpdate()//�����˶�
    {
        //��������׷��
        VectorTracing();
        //���������
        DistanceTracing();
    }
    void Update()
    {
        //�޸Ĺ����泯����
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
        //������ɫ���
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = true;
        }

        //��������Boomerang���
        if (collision.gameObject.CompareTag("Boomerang"))
        {
            MonsterHP -= collision.GetComponent<BoomerangController>().BoomerangDamage;
        }

        //��������Dart���
        if (collision.gameObject.CompareTag("Dart"))
        {
            MonsterHP -= collision.GetComponent<DartController>().DartDamage;
        }
        //��������FireBall���
        if (collision.gameObject.CompareTag("FireBall"))
        {
            MonsterHP -= collision.GetComponent<FireBallManage>().FireBallDamage;
        }
        DestroyManage();

    }
    public void DestroyManage()//����Ѫ��С�ڵ���0��ɾ��
    {
        if (MonsterHP <= 0)
        {
            MonsterHP = RecordHP;
            ReturnBird(monsterName);
            //��ȡ��Ϸʱ�䣬ʱ��Խ����߾�����������Խ��
            //���侭��xpPrefab
            SpawnExp("Exp3");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //�뿪��ɫ���
        if (collision.gameObject.CompareTag("Player"))
        {
            contact = false;
        }
    }

    public void VectorTracing()//��������׷��
    {
        if (!contact && isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }
    }
    public void DistanceTracing()//���������
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= 15)
        {
            ReturnBird(monsterName);
        }
    }
    public IEnumerator IsMoveUpdate()//���¹����Ƿ��ƶ�
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
    private void SpawnExp(string ExpName)//����������
    {
        GameObject ExpObj = monsterPool.GetObjectFromPool(ExpName);
        ExpObj.transform.position = transform.position;
    }
    private void ReturnBird(string monsterName)//����
    {
        monsterPool.ReturnObjectToPool(monsterName, gameObject);
    }
}