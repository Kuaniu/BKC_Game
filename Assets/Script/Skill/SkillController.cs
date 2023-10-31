using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public struct Skill
{
    Texture SkillPic;
    string SkillDetail;
}

public class SkillController : MonoBehaviour
{
    public static SkillController Instance;
    private GameObject _skill_canvas;
    private Transform _skill_first;
    private Transform _skill_second;
    private Transform _skill_third;

    private void Start()
    {
        Instance = this;
        GetSkillComponent();
        SkillBtnEvent();
    }

    private void GetSkillComponent()
    {
        _skill_canvas = GameObject.Find("Canvas/Skill");
        _skill_first = _skill_canvas.transform.GetChild(0);
        _skill_second = _skill_canvas.transform.GetChild(1);
        _skill_third = _skill_canvas.transform.GetChild(2);
    }

    private void SkillBtnEvent()
    {
        Button _skillBtn1 = _skill_first.Find("SkillBtn1").GetComponent<Button>();
        Button _skillBtn2 = _skill_first.Find("SkillBtn2").GetComponent<Button>();
        Button _skillBtn3 = _skill_first.Find("SkillBtn3").GetComponent<Button>();

        _skillBtn1.onClick.AddListener(OnSkillBtn1Click);
        _skillBtn2.onClick.AddListener(OnSkillBtn2Click);
        _skillBtn3.onClick.AddListener(OnSkillBtn3Click);
    }

    private void OnSkillBtn3Click()
    {
        Debug.Log("Skill 3 Onclicked");
    }

    private void OnSkillBtn2Click()
    {
        Debug.Log("Skill 2 Onclicked");
    }

    private void OnSkillBtn1Click()
    {
        Debug.Log("Skill 1 Onclicked");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isShow">是否显示UI</param>
    public void SetSkillUI(bool isShow)
    {
        _skill_canvas.SetActive(isShow);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">技能栏,取值1,2,3分别对应技能</param>
    /// <param name="skill"></param>
    public void SetSkill(int id, Skill skill)
    {
        switch (id)
        {
            default:
                break;
        }
    }

}
