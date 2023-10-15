using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("绑定组件")]
    public GameObject Monster;
    public GameObject[] spawn;
    public List<GameObject> listTemp;
    private int flag;
    private Vector2 monsterPos;
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
            //需要重复执行的代码就放于在此处
            if(TheTime>0.1f)
            {
                TheTime -= 0.1f;

            }
            print(TheTime);
            //设置间隔时间为1秒
            yield return new WaitForSeconds(30f);

        }
    }
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //需要重复执行的代码就放于在此处

            flag = Random.Range(0, 12);
            //生成怪物
            Instantiate(Monster, spawn[flag].transform.position,Quaternion.identity,gameObject.transform);
            //设置间隔时间为1秒
            yield return new WaitForSeconds(TheTime);

        }
    }
}
