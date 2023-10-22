using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private bool isMove=true;

    //��������
    [Header("�ƶ��ٶ�")]
    public float monsterMoveSpeed;
    [Header("��������ֵ")]
    public float MonsterHP;
    [Header("Experience")]
    public GameObject xpPrefab;
    [Header("������")]
    public float Damage;
    void Start()
    {
        //��ȡ��ɫ��transform
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //�ѱ������GameController�еĹ����б���
        //GameObject.Find("GameController").GetComponent<GameController>().MonsterListAdd(gameObject);

        //��ȡ����Renderer
        monsterRenderer = GetComponent<SpriteRenderer>();

        //�����л������Ƿ�����ƶ�
        StartCoroutine("IsMoveUpdate");
    }
    private void FixedUpdate()//�����˶�
    {
        //��������׷��
        VectorTracing();
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
            print("��ɫ����﷢����ײ");
            contact = true;
        }
        //��������Bull���
        if (collision.gameObject.CompareTag("Bull"))
        {
            MonsterHP -= GameObject.Find("Bullet").GetComponent<BullController>().BullDamage;
            StartCoroutine(monsterShake());
            //����Ѫ��С�ڵ���0��ɾ��
            if (MonsterHP <= 0)
            {
                Destroy(gameObject);

                //��ȡ��Ϸʱ�䣬ʱ��Խ����߾�����������Խ��
                //���侭��xpPrefab
                Instantiate(xpPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
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
        if (!contact&&isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }
    }
    public IEnumerator IsMoveUpdate()//���¹����Ƿ��ƶ�
    {
        while(true)
        {
            if(isMove)
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
    public IEnumerator monsterShake()//����������˸
    {
        // ���嶶��Ч���ĳ���ʱ��
        float shakeDuration = 0.2f;

        // ������˸Ч���ĳ���ʱ��
        float blinkDuration = 0.1f;

        // ������˸Ч��֮��ļ��
        float blinkInterval = 0.05f;

        // �ڶ���Ч���ĳ���ʱ���ڣ�����ʼλ�ú����λ��֮����в�ֵ
        for (float t = 0; t < shakeDuration; t += Time.deltaTime)
        {
            // ��˸��ɫ
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

        // ȷ��Sprite�ɼ�
        monsterRenderer.enabled = true;
    }
}