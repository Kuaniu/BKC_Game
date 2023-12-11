using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("��")]
    public GameObject Player;//��ɫ
    public GameObject Bird;//����
    public GameObject Scarecrow;//������
    public GameObject Hedgehog;//��� 

    [Header("����")]
    public GameObject Dart;
    public GameObject Boomerang;
    public GameObject FireBall;
    private bool haveDart;
    private bool haveBoomerang;
    private bool haveFireBall;

    private List<int> excludedNumbers = new List<int>();//�����

    private MonsterPools monsterPool;//�����

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
        //�󶨡���ʼ��
        monsterPool = GetComponent<MonsterPools>();

        haveDart = true;
        haveFireBall = false;


        //����Ϸ���еĵ�n�뿪ʼ��ÿ��n��ִ��һ�κ���OneStages����
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



    //����
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
    private void InstantiateDart()//��������
    {
        Instantiate(Dart, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateBoomerang(bool isDouble)//����������
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
    private void InstantiateFireBall()//��������
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

    //����
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

    //�׶�
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

    //����
    private void ReturnBird(string monsterName, GameObject monsterObj)//����
    {
        monsterPool.ReturnObjectToPool(monsterName, monsterObj);
    }
    private int GenerateRandomNumber(int minRange, int maxRange)//ָ���������Χ
    {
        // ����������ֶ��Ѿ����ų������¿�ʼ
        if (excludedNumbers.Count == (maxRange - minRange))
        {
            excludedNumbers.Clear();
        }

        // ѭ�������������ֱ�����ɵ����ֲ����ų��б���
        int randomInt;
        do
        {
            randomInt = Random.Range(minRange, maxRange);
        } while (excludedNumbers.Contains(randomInt));

        // ����ǰ���ɵ�������ӵ��ų��б���
        excludedNumbers.Add(randomInt);

        return randomInt;
    }
    private void SpawnMonster(string monsterName, Vector3 monsterPos)//�����
    {
        GameObject monsterObj = monsterPool.GetObjectFromPool(monsterName);
        monsterObj.transform.position = monsterPos;
    }

}
