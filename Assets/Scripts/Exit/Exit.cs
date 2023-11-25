using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.Constant;

public class Exit : MonoBehaviour
{
    public static Exit Instance;
    private GameObject _exitui;
    private bool _isExit;

    void Start()
    {
        // Singleton
        Instance = this;

        // GetComponent
        _exitui = GameObject.Find(CanvasConstant.Path_Exit);
        Button _exituibtn = GameObject.Find(CanvasConstant.Path_Exit_Btn).GetComponent<Button>();
        _exituibtn.onClick.AddListener(OnClickExitGame);

        Button _continuebtn = GameObject.Find(CanvasConstant.Path_Continue_Btn).GetComponent<Button>();
        _continuebtn.onClick.AddListener(OnClickContinueGame);

        Button _settingsbtn = GameObject.Find(CanvasConstant.Path_Continue_Btn).GetComponent<Button>();
        _settingsbtn.onClick.AddListener(OnClickSettings);


        _exitui.SetActive(false);
    }

    private void OnClickSettings()
    {
        // throw new NotImplementedException();
    }

    private void OnClickContinueGame()
    {
        Time.timeScale = 1;
        _exitui.SetActive(false);
        _isExit = false;
    }

    private void OnClickExitGame()
    {
        Application.Quit(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isExit)
            {
                OnClickContinueGame();
            }
            else
            {
                Time.timeScale = 0;
                _exitui.SetActive(true);
                _isExit = true;
            }
        }
    }

    public bool IsExit() => _isExit;
}
