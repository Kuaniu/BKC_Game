using UnityEngine;
using UnityEngine.UI;
using Unity.Constant;
using System;

public struct Skill
{
    public Sprite SkillPic;
    public int CurrentLevel;
}

public class SkillController : MonoBehaviour
{
    public static SkillController Instance;
    public GameObject SkillCanvas { get; private set; }
    public GameObject SkillFirst { get; private set; }
    public GameObject SkillFirstLevel { get; private set; }
    public GameObject SkillSecond { get; private set; }
    public GameObject SkillSecondLevel { get; private set; }
    public GameObject SkillThird { get; private set; }
    public GameObject SkillThirdLevel { get; private set; }
    public GameObject SelectedSkills { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetSkillComponent();
        GetSkillLevelComponents();
        SkillBtnEvent();
    }

    private void GetSkillComponent()
    {
        SkillCanvas = GameObject.Find(CanvasConstant.Path_Skill_Canvas);
        SkillFirst = GameObject.Find(CanvasConstant.Path_Skill_First);
        SkillSecond = GameObject.Find(CanvasConstant.Path_Skill_Second);
        SkillThird = GameObject.Find(CanvasConstant.Path_Skill_Third);
        SelectedSkills = GameObject.Find(CanvasConstant.Path_Skill_Selected);

        SetSkillUI(false);
    }

    private void GetSkillLevelComponents()
    {
        SkillFirstLevel = SkillFirst.transform.GetChild(0).gameObject;
        SkillSecondLevel = SkillSecond.transform.GetChild(0).gameObject;
        SkillThirdLevel = SkillThird.transform.GetChild(0).gameObject;
    }

    private void SkillBtnEvent()
    {
        Button _skillBtn1 = SkillFirst.GetComponent<Button>();
        Button _skillBtn2 = SkillSecond.GetComponent<Button>();
        Button _skillBtn3 = SkillThird.GetComponent<Button>();

        _skillBtn1.onClick.AddListener(OnSkillBtn1Click);
        _skillBtn2.onClick.AddListener(OnSkillBtn2Click);
        _skillBtn3.onClick.AddListener(OnSkillBtn3Click);
    }

    private void OnSkillBtn3Click()
    {
        SetSkillUI(false);
    }

    private void OnSkillBtn2Click()
    {
        SetSkillUI(false);
        GameObject.Find("GameController").GetComponent<GameController>().BoomerangUpLevel();

    }

    private void OnSkillBtn1Click()
    {
        SetSkillUI(false);
        GameObject.Find("GameController").GetComponent<GameController>().DartUpLevel();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isShow">是否显示UI</param>
    public void SetSkillUI(bool isShow)
    {
        if (isShow)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;
        SkillCanvas.SetActive(isShow);
    }

    public void SetSkillFirst(Skill skill)
    {
        SkillFirst.GetComponent<Image>().sprite = skill.SkillPic;
        if (skill.CurrentLevel > 0)
        {
            for (int i = 0; i < skill.CurrentLevel; i++)
            {
                SkillFirstLevel.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

    }

    public void SetSkillSecond(Skill skill)
    {
        SkillSecond.GetComponent<Image>().sprite = skill.SkillPic;
        for (int i = 0; i < skill.CurrentLevel; i++)
        {
            SkillSecondLevel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetSkillThird(Skill skill)
    {
        SkillThird.GetComponent<Image>().sprite = skill.SkillPic;
        for (int i = 0; i < skill.CurrentLevel; i++)
        {
            SkillThirdLevel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
