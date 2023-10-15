using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    private Transform player;
    private bool contact;
    SpriteRenderer monsterRenderer;

    //��������
    [Header("�ƶ��ٶ�")]
    public float monsterMoveSpeed;
    [Header("����ֵ")]
    public float MonsterHP;
    [Header("Experience")]
    public GameObject xpPrefab;
    //[Header("������")]
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

        //��δ�Ӵ���ɫ�������λ��ǰ��
        if(!contact)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, monsterMoveSpeed * Time.deltaTime);
        }



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("��ɫ����﷢����ײ");
            contact = true;
        }
        if (collision.gameObject.CompareTag("Bull"))
        {
            MonsterHP -= 2;
            StartCoroutine(monsterShake());
            if(MonsterHP<=0)
            {
                Destroy(gameObject);

                //��ȡ��Ϸʱ�䣬ʱ��Խ����߾�����������Խ��
                Quaternion quaternion = Quaternion.identity;            
                //���侭��xpPrefab
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
    //����������˸
    public IEnumerator monsterShake()
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
