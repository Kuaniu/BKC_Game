using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [Header("��ɫ�ƶ��ٶ�")]
    public float PlayerMoveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        //��ɫ�ƶ�����
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H * PlayerMoveSpeed *Time.deltaTime, V * PlayerMoveSpeed*Time.deltaTime));
    }
}
