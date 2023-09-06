using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    [Header("角色移动速度")]
    public float PlayerMoveSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        //角色移动控制
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H * PlayerMoveSpeed *Time.deltaTime, V * PlayerMoveSpeed*Time.deltaTime));
    }
}
