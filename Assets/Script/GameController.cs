using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameController : MonoBehaviour
{
    [Header("绑定组件")]
    public GameObject Bird;//鸡仔
    public GameObject Scarecrow;//稻草人
    public GameObject Hedgehog;//刺猬 
    public GameObject Player;//角色

    [Header("武器预制体")]
    public GameObject Boomerang;
    public bool haveBoomerang;

    [Header("怪物生成点")]
    public GameObject[] spawn;
    private int flag;

    [Header("所有怪物")]
    public List<Transform> listTemp;



    void Start()
    {
        //从游戏运行的第n秒开始，每隔n秒执行一次函数rightRayCollider函数
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);
        listTemp = new List<Transform>();
        flag = 0;

        WeaponGeneration();

    }
    void Update()
    {
        TextStages();
        FindClosestMonster();

    }

    private void WeaponGeneration()
    {
        if (haveBoomerang)
        {
            InvokeRepeating("InstantiateBoomerang", 0, 4f);
        }

    }
    private void InstantiateBoomerang()
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    public void MonsterListAdd(Transform Monster)//将Monster加入列表里
    {
        listTemp.Add(Monster);
    }

    private void OneStages()
    {
        flag = Random.Range(0, 12);
        Instantiate(Bird, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
    }
    private void TwoStages()
    {
        CancelInvoke("OneStages");
        flag = Random.Range(0, 12);
        Instantiate(Bird, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
        flag = Random.Range(0, 12);
        Instantiate(Scarecrow, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
    }
    private void ThreeStages()
    {
        CancelInvoke("TwoStages");
        flag = Random.Range(0, 12);
        Instantiate(Bird, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
        flag = Random.Range(0, 12);
        Instantiate(Scarecrow, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);
        flag = Random.Range(0, 12);
        Instantiate(Hedgehog, spawn[flag].transform.position, Quaternion.identity, gameObject.transform);

    }
    private void FourStages()
    {
        CancelInvoke("ThreeStages");
        for (int i = 0; i < 12; i++)
        {
            Instantiate(Bird, spawn[i].transform.position, Quaternion.identity, gameObject.transform);
        }

    }
    private void TextStages()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            OneStages();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            TwoStages();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ThreeStages();
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            FourStages();
        }
    }

    public Vector3 FindClosestMonster()
    {
        Transform closestMonster = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = Player.transform.position;

        foreach (Transform potentialTarget in listTemp)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestMonster = potentialTarget;
            }
        }

        if (closestMonster != null)
        {
            // 在这里可以处理最近的怪物
            return closestMonster.transform.position;
        }
        return Vector3.zero;
    }

    //private IEnumerator FourStages()
    //{
    //    while (true)
    //    {
    //        //需要重复执行的代码就放于在此处
    //        //设置间隔时间为1秒
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
