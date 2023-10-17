using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�����")]
    public GameObject Bird;
    public GameObject Scarecrow;
    public GameObject[] spawn;
    //public List<GameObject> listTemp;
    private int flag;
    private float TheTime;
    void Start()
    {
        TheTime = 1;
        StartCoroutine("Time");
        StartCoroutine("GenerateMonsters");
        flag = 0;
    }
    void Update()
    {
        
    }
    //public void MonsterListAdd(GameObject Monster)//��Monster�����б���
    //{
    //    listTemp.Add(Monster);
    //}
    IEnumerator Time()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
            if(TheTime>0.1f)
            {
                TheTime -= 0.1f;

            }
            //print(TheTime);
            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(5f);

        }
    }
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
            print(TheTime);
            //���ɹ���
            if (TheTime>=0.9f)
            {
                flag = Random.Range(0, 12);
                Instantiate(Bird, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
            }
            else
            {
                flag = Random.Range(0, 12);
                Instantiate(Bird, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
                flag = Random.Range(0, 12);
                Instantiate(Scarecrow, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
            }

            //���ü��ʱ��Ϊ1��
            yield return new WaitForSeconds(TheTime);

        }
    }
}
