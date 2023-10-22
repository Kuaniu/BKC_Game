using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("�����")]
    public GameObject Bird;//����
    public GameObject Scarecrow;//������
    public GameObject Hedgehog;//��� 

    public GameObject[] spawn;
    //public List<GameObject> listTemp;
    private int flag = 0;
    void Start()
    {
        //����Ϸ���еĵ�n�뿪ʼ��ÿ��n��ִ��һ�κ���rightRayCollider����
        InvokeRepeating("OneStages", 0, 1);
        InvokeRepeating("TwoStages", 30, 1);
        InvokeRepeating("ThreeStages", 60, 1);
        InvokeRepeating("FourStages", 120, 1);
    }
    void Update()
    {

    }
    //public void MonsterListAdd(GameObject Monster)//��Monster�����б���
    //{
    //    listTemp.Add(Monster);
    //} 
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
        for(int i =0;i<12;i++)
        {
            Instantiate(Bird, spawn[i].transform.position, Quaternion.identity, gameObject.transform);
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
