using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;//HP�ĵ�����Ҫ���õ�UIԴ�ļ�

public class HPcontroller : MonoBehaviour
{
    [Header("�����")]
    public Slider PlayerHP;

    void Start()
    {
        PlayerHP.value = 1;
    }

    void Update()
    {   

        //��Ѫ��Ϊ0���ɫ����
        if (PlayerHP.value == 0)
        {
            //��Ϸ��ͣ
            PlayerHP.gameObject.SetActive(false);//����Ѫ��UI
            Time.timeScale = 0;
            //���������������л�����/�����˵�
        }
        //����Ѫ���仯
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerHP.value -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHP.value += 0.1f;
        }
    }

    //��Ѫ��������
    public void PlayerAdd(float increase)
    {
        PlayerHP.value += increase * 0.1f;
    }

    //��Ѫ��������
    public  void PlayerBuckle(float reduce)
    {
        PlayerHP.value -= reduce * 0.1f;
    }
}
