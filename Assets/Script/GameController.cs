using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("绑定组件")]
    public GameObject Monster;
    public GameObject[] spawn;
    public List<GameObject> listTemp;
    private int flag;
    void Start()
    {
        StartCoroutine("GenerateMonsters");
        //delayTime = 0.1f;
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
    IEnumerator GenerateMonsters()
    {

        while (true)
        {
            //需要重复执行的代码就放于在此处
            flag = Random.Range(0, 12);
            Instantiate(Monster, spawn[flag].transform);
            //设置间隔时间为1秒
            yield return new WaitForSeconds(1);

        }
    }
}
