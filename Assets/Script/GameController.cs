using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�����")]
    public GameObject Monster;
    public GameObject[] spawn;
    private float delayTime;
    private int flag;
    void Start()
    {
        StartCoroutine("GenerateMonsters");
        delayTime = 0.1f;
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
            flag = Random.Range(0, 12);
            Instantiate(Monster, spawn[flag].transform);
            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(1);

        }
    }
}
