using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;//HP的调用需要引用的UI源文件


public class PlayerController : MonoBehaviour
{
    [Header("移动速度")]
    public float PlayerMoveSpeed;
    [Header("生命值")]
    public Slider PlayerHP;
    //[Header("攻击力")]
    //public float Damage;

    void Start()
    {
        PlayerHP.value = 1;
    }

    void Update()
    {
        //移动
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H * PlayerMoveSpeed * Time.deltaTime, V * PlayerMoveSpeed * Time.deltaTime));

        //生命值
        if (PlayerHP.value == 0)
        {
            //游戏暂停
            PlayerHP.gameObject.SetActive(false);//隐藏血条UI
            Time.timeScale = 0;
            //播放死亡动画，切换场景/弹出菜单
        }
        

        //测试血条变化
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
        //若角色碰到怪物则扣血
        PlayerHP.value -= 0.1f;
    }

}
