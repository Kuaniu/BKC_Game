using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameController : MonoBehaviour
{
    [Header("�����")]
    public GameObject Bird;//����
    public GameObject Scarecrow;//������
    public GameObject Hedgehog;//��� 
    public GameObject Player;//��ɫ

    [Header("����Ԥ����")]
    public GameObject Boomerang;//������
    private bool haveBoomerang;

    public GameObject Dart;//�ɵ�
    private bool haveDart;

    public GameObject FireBall;
    private bool haveFireBall;

    [Header("�������ɵ�")]
    public GameObject[] spawn;
    private int flag;

    [Header("���й���")]
    public List<GameObject> listTemp;

    private List<int> excludedNumbers = new List<int>();//�����

    void Start()
    {
        //����Ϸ���еĵ�n�뿪ʼ��ÿ��n��ִ��һ�κ���OneStages����
        //InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 0, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);
        listTemp = new List<GameObject>();
        flag = 0;

        haveBoomerang = true;
        haveDart= true;
        haveFireBall = true;

        WeaponGeneration();//��ɫӵ�е���������
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
    private void InstantiateBoomerang()//����������
    {
        Instantiate(Boomerang, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateDart()//��������
    {
        Instantiate(Dart, Player.transform.position, Quaternion.identity, gameObject.transform);
    }
    private void InstantiateFireBall()//��������
    {
        Instantiate(FireBall,Player.transform);
    }
    public void MonsterListAdd(GameObject Monster)//��Monster�����б���
    {
        listTemp.Add(Monster);
    }
    public GameObject FindClosestMonster()//�ҵ������ɫ����Ĺ���
    {
        Transform closestMonster = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = Player.transform.position;

        foreach (GameObject potentialTarget in listTemp)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestMonster = potentialTarget.transform;
            }
        }

        if (closestMonster != null)
        {
            // ��������Դ�������Ĺ���
            return closestMonster.gameObject;
        }
        return null;
    }
    private void UpFormation(int Count)
    {
        for(int i=0;i<Count;i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            var obj = Instantiate(Bird,gameObject.transform);
            obj.transform.localPosition = new Vector2(Rnum, 6);
        }
    }
    private void DownFormation(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-10, 10);
            var obj = Instantiate(Bird, gameObject.transform);
            obj.transform.localPosition = new Vector2(Rnum, -6);
        }
    }
    private void LeftFormation(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-6, 6);
            var obj = Instantiate(Bird, gameObject.transform);
            obj.transform.localPosition = new Vector2(-10, Rnum);
        }
    }
    private void RightFormation(int Count)
    {
        for (int i = 0; i < Count; i++)
        {
            int Rnum = GenerateRandomNumber(-6, 6);
            var obj = Instantiate(Bird, gameObject.transform);
            obj.transform.localPosition = new Vector2(10, Rnum);
        }
    }
    private int GenerateRandomNumber(int minRange,int maxRange)//ָ���������Χ
    {
        // ����������ֶ��Ѿ����ų������¿�ʼ
        if (excludedNumbers.Count == (maxRange - minRange))
        {
            Debug.LogWarning("All numbers have been excluded. Resetting exclusion list.");
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
    private void OneStages()
    {
        UpFormation(1);
        DownFormation(1);
        LeftFormation(1);
        RightFormation(1);
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
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            SkillController.Instance.SetSkillUI(true);
        }
        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            SkillController.Instance.SetSkillUI(false);
        }
    }
    //private IEnumerator FourStages()
    //{
    //    while (true)
    //    {
    //        //��Ҫ�ظ�ִ�еĴ���ͷ����ڴ˴�
    //        //���ü��ʱ��Ϊ1��
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}
