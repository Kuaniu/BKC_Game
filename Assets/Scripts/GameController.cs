using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
    private float DartTime = 1;

    void Start()
    {
        //�󶨡���ʼ��
        monsterPool = GetComponent<MonsterPools>();

        haveBoomerang = false;
        haveDart = true;
        haveFireBall = false;


        //����Ϸ���еĵ�n�뿪ʼ��ÿ��n��ִ��һ�κ���OneStages����
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);

        WeaponGeneration();//��ɫӵ�е���������
    }
    void Update()
    {
        TextStages();
    }

    public void DartUpLevel()
    {
        DartController.DartLevel += 1;
        switch (DartController.DartLevel)
        {
            case 1:
                haveDart = true;
                break;
            case 2:
                DartController.DamageDouble();
                break;
            case 3:
                DartController.MoveSpeedUp();
                break;
            case 4:
                DartController.MoveSpeedUp();
                break;
        }
    }

    //����
    private void WeaponGeneration()
    {
        if(Time.time- DartTimeR >= DartTime)
        {
            InstantiateDart();
            DartTimeR = Time.time;
        }
        if (haveDart)
        {
            InvokeRepeating("InstantiateDart", 0, 1f);
        }

        if (haveBoomerang)
        {
            InvokeRepeating("InstantiateBoomerang", 0, 3f);
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
    private void InstantiateBoomerang()//����������
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateFireBall()//��������
    {
        Instantiate(FireBall, Player.transform);
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
