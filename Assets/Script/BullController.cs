using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    private float BullMoveSpeed;
    private Vector3 target;
    private float MonsterHP;
    void Start()
    {
        BullMoveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").GetComponent<PlayerController>().bullpos.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, BullMoveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 10));
        MonsterHP = GameObject.Find("Monster(Clone)").GetComponent<MonsterController>().MonsterHP;
        StartCoroutine("DamageSpeed");

    }

    IEnumerator DamageSpeed()
    {

        while (true)
        {
            //ÿ0.5s�۳�����1Ѫ
            if (MonsterHP == 0)
            {

            }
            else
            {
                MonsterHP -= 1;
            }

            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(0.5f);

        }
    }
}
