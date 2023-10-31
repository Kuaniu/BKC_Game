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

    [Header("�������ɵ�")]
    public GameObject[] spawn;
    private int flag;

    [Header("���й���")]
    public List<Transform> listTemp;

    void Start()
    {
        //����Ϸ���еĵ�n�뿪ʼ��ÿ��n��ִ��һ�κ���OneStages����
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);
        listTemp = new List<Transform>();
        flag = 0;

        haveBoomerang = true;
        haveDart= true;

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
    private void WeaponGeneration()
    {
        if (haveBoomerang)
        {
            InvokeRepeating("InstantiateBoomerang",0, 3f);
        }
        if(haveDart)
        {
            InvokeRepeating("InstantiateDart", 0, 1f);
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
    public void MonsterListAdd(Transform Monster)//��Monster�����б���
    {
        listTemp.Add(Monster);
    }
    public Vector3 FindClosestMonster()//�ҵ������ɫ����Ĺ���
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
            // ��������Դ�������Ĺ���
            return closestMonster.transform.position;
        }
        return Vector3.zero;
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
