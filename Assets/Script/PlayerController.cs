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

    public GameObject bullpos;
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

        bullpos=GetNearestGameObject(GameObject.Find("GameController").GetComponent<GameController>().listTemp);

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

    GameObject GetNearestGameObject(List<GameObject> listTemp)
    {
        
        if (listTemp != null && listTemp.Count > 0)
        {
            GameObject targetTemp = listTemp.Count > 0 ? listTemp[0] : null;
            float dis = Vector3.Distance(transform.position, listTemp[0].transform.position);
            float disTemp;
            for (int i = 1; i < listTemp.Count; i++)
            {
                disTemp = Vector3.Distance(transform.position, listTemp[i].transform.position);
                if (disTemp < dis)
                {
                    targetTemp = listTemp[i];
                    dis = disTemp;
                }
            }
            return targetTemp;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����ɫ�����������Ѫ
        PlayerHP.value -= 0.1f;
    }

}
