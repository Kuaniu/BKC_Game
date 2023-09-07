using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;//HP的调用需要引用的UI源文件

public class HPcontroller : MonoBehaviour
{
    [Header("绑定组件")]
    public Slider PlayerHP;

    void Start()
    {
        PlayerHP.value = 1;
    }

    void Update()
    {   

        //若血量为0则角色死亡
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

    //加血方法主角
    public void PlayerAdd(float increase)
    {
        PlayerHP.value += increase * 0.1f;
    }

    //扣血方法主角
    public  void PlayerBuckle(float reduce)
    {
        PlayerHP.value -= reduce * 0.1f;
    }
}
