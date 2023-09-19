using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;//HP�ĵ�����Ҫ���õ�UIԴ�ļ�


public class PlayerController : MonoBehaviour
{
    [Header("�ƶ��ٶ�")]
    public float PlayerMoveSpeed;
    [Header("����ֵ")]
    public Slider PlayerHP;
    //[Header("������")]
    //public float Damage;

    void Start()
    {
        PlayerHP.value = 1;
    }

    void Update()
    {
        //�ƶ�
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H * PlayerMoveSpeed * Time.deltaTime, V * PlayerMoveSpeed * Time.deltaTime));

        //����ֵ
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


    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����ɫ�����������Ѫ
        PlayerHP.value -= 0.1f;
    }

}
