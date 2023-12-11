using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    private float DartTimeR = 0;
    private float BoomerangTimeR = 0;

    private float DartTime = 0.5f;
    private float BoomerangTime = 2;

    private int DartLevel = 1;
    private int BoomerangLevel = 0;
    private int FireBallLevel = 0;

    private int FireBallNum = 1;

    private bool isBoomerangDouble = false;

    void Start()
    {
        //绑定、初始化
        monsterPool = GetComponent<MonsterPools>();

        haveDart = true;
        haveFireBall = false;


        //从游戏运行的第n秒开始，每隔n秒执行一次函数OneStages函数
        InvokeRepeating("OneStages", 0, 2);
        InvokeRepeating("TwoStages", 30, 4);

        InvokeRepeating("BirdBirdBird", 60, 2);

        InvokeRepeating("ThreeStages", 120, 8);
        InvokeRepeating("FourStages", 180, 10);

    }
    void Update()
    {
        WeaponSpawn();
        TextStages();
    }



    //武器
    private void WeaponSpawn()
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
        if (haveFireBall)
        {
            InstantiateFireBall();
        }

    }
    private void InstantiateDart()//飞镖生成
    {
        Instantiate(Dart, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateBoomerang(bool isDouble)//回旋镖生成
    {
        if (isDouble)
        {
            Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
            Invoke("DoubleBoomerang", 0.1f);
        }
        else
        {
            Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
        }
    }
    private void InstantiateFireBall()//火球生成
    {
        if (FireBallNum < 2)
        {
            Instantiate(FireBall, Player.transform);
            FireBallNum++;
        }
    }
    private void DoubleBoomerang()
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
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
                print("DartTime =0.45s");
                DartTime = 0.45f;
                DartController.MoveSpeedUp();
                break;
            case 3:
                print("DartTime =0.3s");
                DartTime = 0.3f;
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
    public void FireBallUpLevel()
    {
        FireBallLevel += 1;
        switch (FireBallLevel)
        {
            case 1:
                print("HaveFireBall");
                haveFireBall = true;
                break;
            case 2:
                print("FireBallDamageUp");
                FireBallManage.SetDamage();
                break;
            case 3:
                print("FireBallSpeedUp");
                FireBallManage.SetMoveSpeed();
                break;
            case 4:
                print("FireBallBig");
                FireBallManage.SetScale();
                break;
        }
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

        CancelInvoke("OneStages");
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

        CancelInvoke("BirdBirdBird");
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

        CancelInvoke("ThreeStages");
    }
    private void BirdBirdBird()
    {
        UpFormation("Bird", 10);
        DownFormation("Bird", 10);
        LeftFormation("Bird", 10);
        RightFormation("Bird", 10);

        CancelInvoke("TwoStages");
    }
    private void TextStages()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Skill skill = new Skill()
            {
                SkillPic = null,
                CurrentLevel = 1
            };
            SkillController.Instance.SetSkillFirst(skill);
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
