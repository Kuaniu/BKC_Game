using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    private GameObject _exitui;
    private bool _isExit;

    void Start()
    {
        _exitui = GameObject.Find(CanvasConstant.Path_Exit);
        Button _exituibtn = GameObject.Find(CanvasConstant.Path_Exit_Btn).GetComponent<Button>();

        _exituibtn.onClick.AddListener(OnClickExitGame);

        _exitui.SetActive(false);
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
                Time.timeScale = 1;
                _exitui.SetActive(false);
                _isExit = false;
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
