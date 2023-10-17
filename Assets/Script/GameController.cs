using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("绑定组件")]
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
    //public void MonsterListAdd(GameObject Monster)//将Monster加入列表里
    //{
    //    listTemp.Add(Monster);
    //}
    IEnumerator Time()
    {

        while (true)
        {
            //需要重复执行的代码就放于在此处
            if(TheTime>0.1f)
            {
                TheTime -= 0.1f;

            }
            //print(TheTime);
            //设置间隔时间为1秒
            yield return new WaitForSeconds(5f);

        }
    }
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //需要重复执行的代码就放于在此处
            print(TheTime);
            //生成怪物
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

            //设置间隔时间为1秒
            yield return new WaitForSeconds(TheTime);

        }
    }
}
