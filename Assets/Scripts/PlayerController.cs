using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.Mathematics;
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
    public float HPcount;

    //获取怪物Player
    private SpriteRenderer PlayerRenderer;
    //[Header("攻击力")]
    //public float Damage;
    //经验值
    private float Experience;
    //动画组件
    private Animator animator;
    //角色等级
    private int CharacterLevel;

    void Start()
    {
        PlayerHP.value = 1;
        Experience = 0;
        CharacterLevel = 0;
        PlayerRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    //物理运动更新
    private void FixedUpdate()
    {
        playerMove();
    }
    void Update()
    {
        playerHp();
        UpdateCharacterLevel();

        float H = Input.GetAxisRaw("Horizontal");
        //修改角色面朝方向
        if (H==-1)
        {
            animator.SetBool("isFlip", false);
        }
        if (H==1)
        {
            animator.SetBool("isFlip", true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //若角色碰到怪物则扣血
        if (collision.gameObject.CompareTag("Monster"))
        {
            PlayerHP.value -= collision.GetComponent<MonsterController>().Damage / HPcount;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)//触发器接触检测
    {
        //经验球功能
        //经验球与角色距离<=1.5则经验球向角色飞行
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 1.5f)
        {
            collision.transform.position = Vector3.MoveTowards(collision.transform.position, transform.position, 5 * Time.deltaTime);
        }
        //拾取经验球
        if (collision.gameObject.CompareTag("Experience") && Vector3.Distance(collision.transform.position, transform.position) <= 0.6f)
        {
            Experience += collision.gameObject.GetComponent<Experience>().xp;
            Destroy(collision.gameObject);
        }
    }
    void playerMove()//角色移动 

    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        transform.Translate(new Vector2(H, V).normalized  * PlayerMoveSpeed * Time.deltaTime);
    }
    void playerHp()//角色生命值

    {
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
    void UpdateCharacterLevel()//更新角色等级
    {
        
        int i = CharacterLevel;print(i);
        if((int)Math.Log(Experience, 2) > i)
        {
            SkillController.Instance.SetSkillUI(true);
            CharacterLevel = (int)Math.Log(Experience, 2);
        }
        //if(CharacterLevel>i)
        //{
        //    SkillController.Instance.SetSkillUI(true);
        //}
    }
}
