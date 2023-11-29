using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("绑定")]
    public GameObject Player;//角色
    public GameObject Bird;//鸡仔
    public GameObject Scarecrow;//稻草人
    public GameObject Hedgehog;//刺猬 

    [Header("武器")]
    public GameObject Dart;
    public GameObject Boomerang;
    public GameObject FireBall;
    private bool haveDart;
    private bool haveBoomerang;
    private bool haveFireBall;

    private List<int> excludedNumbers = new List<int>();//随机数

    private MonsterPools monsterPool;//怪物池

    private int DartLevel = 1;
    private float DartTimeR = 0;
    private float DartTime = 0.5f;
    private int BoomerangLevel = 0;
    private float BoomerangTimeR = 0;
    private float BoomerangTime = 2;
    private bool isBoomerangDouble = false;

    void Start()
    {
        //绑定、初始化
        monsterPool = GetComponent<MonsterPools>();

        haveDart = true;
        haveFireBall = false;


        //从游戏运行的第n秒开始，每隔n秒执行一次函数OneStages函数
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);

    }
    void Update()
    {
        WeaponGeneration();//角色拥有的武器生成

        TextStages();
    }

    public void DartUpLevel()
    {
        DartLevel += 1;
        switch (DartLevel)
        {
            case 1:
                print("Dart is true");
                haveDart = true;
                break;
            case 2:
                print("DartTime =0.4s");
                DartTime = 0.4f;
                DartController.MoveSpeedUp();
                break;
            case 3:
                print("DartTime =0.2s");
                DartTime = 0.2f;
                DartController.MoveSpeedUp();
                break;
            case 4:
                print("Dart damage double");
                DartController.DamageDouble();
                break;
        }
    }
    public void BoomerangUpLevel()
    {
        BoomerangLevel += 1;
        switch (BoomerangLevel)
        {
            case 1:
                print("Boomerang is true");
                haveBoomerang = true;
                break;
            case 2:
                print("Boomerang num is double");
                isBoomerangDouble = true;
                break;
            case 3:
                print("BoomerangTime -=0.2s");
                BoomerangTime -= 0.2f;
                BoomerangController.MoveSpeedUp();
                break;
            case 4:
                print("Boomerang damage double");
                BoomerangController.BoomerangDamageDouble();
                break;
        }
    }
    //武器
    private void WeaponGeneration()
    {
        if (haveDart && Time.time - DartTimeR >= DartTime)
        {
            InstantiateDart();
            DartTimeR = Time.time;
        }
        if (haveBoomerang && Time.time - BoomerangTimeR >= BoomerangTime)
        {
            InstantiateBoomerang(isBoomerangDouble);
            BoomerangTimeR = Time.time;
        }
        //if (haveDart)
        //{
        //    InvokeRepeating("InstantiateDart", 0, 1f);
        //}

        //if (haveBoomerang)
        //{
        //    InvokeRepeating("InstantiateBoomerang", 0, 3f);
        //}
        //if (haveFireBall)
        //{
        //    InstantiateFireBall();
        //}

    }

    private void InstantiateDart()//飞镖生成
    {
        Instantiate(Dart, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateBoomerang(bool isDouble)//回旋镖生成
    {
        if(isDouble)
        {
            Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
            Invoke("doubleBoomerang", 0.2f);
        }
        else
        {
            Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
        }
    }
    private void InstantiateFireBall()//火球生成
    {
        Instantiate(FireBall, Player.transform);
    }

    private void DoubleBoomerang()
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    //阵列
    private void UpFormation(string monsterName, int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            SpawnMonster(monsterName, new Vector3(Rnum, 6, 0) + Player.transform.position);
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
            SpawnMonster(monsterName, new Vector3(-10, Rnum, 0) + Player.transform.position);
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

    //阶段
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

    //功能
    private void ReturnBird(string monsterName, GameObject monsterObj)//回收
    {
        monsterPool.ReturnObjectToPool(monsterName, monsterObj);
    }
    private int GenerateRandomNumber(int minRange, int maxRange)//指定随机数范围
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
    private void SpawnMonster(string monsterName, Vector3 monsterPos)//对象池
    {
        GameObject monsterObj = monsterPool.GetObjectFromPool(monsterName);
        monsterObj.transform.position = monsterPos;
    }

}
