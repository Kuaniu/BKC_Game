using UnityEngine;
using UnityEngine.UI;
using Unity.Constant;

public struct Skill
{
    public Sprite SkillPic;
    public uint CurrentLevel;
}

public class SkillController : MonoBehaviour
{
    public static SkillController Instance;
    public GameObject SkillCanvas { get; private set; }
    public GameObject SkillFirst { get; private set; }
    public GameObject SkillSecond { get; private set; }
    public GameObject SkillThird { get; private set; }
    public GameObject SelectedSkills { get; private set; }

    private void Start()
    {
        Instance = this;
        GetSkillComponent();
        SkillBtnEvent();
    }

    private void GetSkillComponent()
    {
        SkillCanvas = GameObject.Find(CanvasConstant.Path_Skill_Canvas);
        SkillFirst = GameObject.Find(CanvasConstant.Path_Skill_First);
        SkillSecond = GameObject.Find(CanvasConstant.Path_Skill_Second);
        SkillThird = GameObject.Find(CanvasConstant.Path_Skill_Third);
        SelectedSkills = GameObject.Find(CanvasConstant.Path_Skill_Canvas + "/SelectedSkills");

        SetSkillUI(false);
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
        Debug.Log("Skill 3 Onclicked");
    }

    private void OnSkillBtn2Click()
    {
        SetSkillUI(false);
        Debug.Log("Skill 2 Onclicked");
    }

    private void OnSkillBtn1Click()
    {
        SetSkillUI(false);
        GameObject.Find("GameController").GetComponent<GameController>().DartUpLevel();
        Debug.Log("Skill 1 Onclicked");
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
    }
}
