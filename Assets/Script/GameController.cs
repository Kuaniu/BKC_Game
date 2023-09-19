using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�����")]
    public GameObject Monster;
    public GameObject[] spawn;
    public List<GameObject> listTemp;
    private int flag;
    private float TheTime;
    void Start()
    {
        TheTime = 1;
        StartCoroutine("Time");
        StartCoroutine("GenerateMonsters");
        flag = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MonsterListAdd(GameObject Monster)
    {
        listTemp.Add(Monster);
    }
    IEnumerator Time()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
            if(TheTime>0.1f)
            {
                TheTime -= 0.1f;

            }
            print(TheTime);
            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(30f);

        }
    }
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�

            flag = Random.Range(0, 12);
            Instantiate(Monster, spawn[flag].transform);
            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(TheTime);

        }
    }
}
