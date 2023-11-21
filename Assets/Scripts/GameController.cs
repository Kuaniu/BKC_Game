using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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
    public GameObject Boomerang;//回旋镖
    private bool haveBoomerang;

    public GameObject Dart;//飞刀
    private bool haveDart;

    public GameObject FireBall;
    private bool haveFireBall;

    private List<int> excludedNumbers = new List<int>();//随机数
    private MonsterPools monsterPool;

    void Start()
    {
        //绑定、初始化
        monsterPool = GetComponent<MonsterPools>();

        haveBoomerang = true;
        haveDart = true;
        haveFireBall = true;

        //从游戏运行的第n秒开始，每隔n秒执行一次函数OneStages函数
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);

        WeaponGeneration();//角色拥有的武器生成
    }
    void Update()
    {
        TextStages();
    }
    public void SetHaveBoomerang(bool isHave)
    {
        haveBoomerang=isHave;
    }
    public void SetHaveDart(bool isHave)
    {
        haveDart= isHave;
    }
    public void SetHaveFireBall(bool isHave)
    {
        haveFireBall = isHave;
    }
    private void WeaponGeneration()
    {

        if (haveBoomerang)
        {
            InvokeRepeating("InstantiateBoomerang", 0, 3f);
        }
        if (haveDart)
        {
            InvokeRepeating("InstantiateDart", 0, 1f);
        }
        if (haveFireBall)
        {
            InstantiateFireBall();
        }

    }
    private void InstantiateBoomerang()//回旋镖生成
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateDart()//飞镖生成
    {
        Instantiate(Dart, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateFireBall()//火球生成
    {
        Instantiate(FireBall,Player.transform);
    }
    private void UpFormation(string monsterName ,int Count)
    {
        for(int i=0;i<Count;i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            SpawnMonster(monsterName, new Vector3(Rnum, 6,0)+Player.transform.position);
        }
    }
    private void DownFormation(string monsterName, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            SpawnMonster(monsterName, new Vector3(Rnum, -6, 0) + Player.transform.position);
        }
    }
    private void LeftFormation(string monsterName, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            SpawnMonster(monsterName, new Vector3(-10, Rnum,0) + Player.transform.position);
        }
    }
    private void RightFormation(string monsterName, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            SpawnMonster(monsterName, new Vector3(10, Rnum, 0) + Player.transform.position);
        }
    }
    private int GenerateRandomNumber(int minRange,int maxRange)//指定随机数范围
    {
        // 如果所有数字都已经被排除，重新开始
        if (excludedNumbers.Count == (maxRange - minRange))
        {
            excludedNumbers.Clear();
        }

        // 循环生成随机数，直到生成的数字不在排除列表中
        int randomInt;
        do
        {
            randomInt = Random.Range(minRange, maxRange);
        } while (excludedNumbers.Contains(randomInt));

        // 将当前生成的数字添加到排除列表中
        excludedNumbers.Add(randomInt);

        return randomInt;
    }
    private void OneStages()
    {
        UpFormation("Bird", 1);
        DownFormation("Bird", 1);
        LeftFormation("Bird", 1);
        RightFormation("Bird", 1);
    }
    private void TwoStages()
    {
        UpFormation("Bird", 1);
        DownFormation("Bird", 1);
        LeftFormation("Bird", 1);
        RightFormation("Bird", 1);

        UpFormation("Sca", 1);
        DownFormation("Sca", 1);
        LeftFormation("Sca", 1);
        RightFormation("Sca", 1);

    }
    private void ThreeStages()
    {
        UpFormation("Bird", 1);
        DownFormation("Bird", 1);
        LeftFormation("Bird", 1);
        RightFormation("Bird", 1);

        UpFormation("Sca", 1);
        DownFormation("Sca", 1);
        LeftFormation("Sca", 1);
        RightFormation("Sca", 1);

        UpFormation("Hed", 1);
        DownFormation("Hed", 1);
        LeftFormation("Hed", 1);
        RightFormation("Hed", 1);

    }
    private void FourStages()
    {
        UpFormation("Bird", 1);
        DownFormation("Bird", 1);
        LeftFormation("Bird", 1);
        RightFormation("Bird", 1); ;

        UpFormation("Sca", 1);
        DownFormation("Sca", 1);
        LeftFormation("Sca", 1);
        RightFormation("Sca", 1);

        UpFormation("Hed", 1);
        DownFormation("Hed", 1);
        LeftFormation("Hed", 1);
        RightFormation("Hed", 1);
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
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SkillController.Instance.SetSkillUI(true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SkillController.Instance.SetSkillUI(false);
        }
    }
    private void SpawnMonster(string monsterName,Vector3 monsterPos)//对象池
    {
        GameObject monsterObj = monsterPool.GetObjectFromPool(monsterName);
        monsterObj.transform.position = monsterPos;
    }
    private void ReturnBird(string monsterName, GameObject monsterObj)//回收
    {
        monsterPool.ReturnObjectToPool(monsterName, monsterObj);
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
