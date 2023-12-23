using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBackGround : MonoBehaviour
{
    public GameObject Map1;
    public GameObject Map2;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Map1.SetActive(true);
            Map2.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Map1.SetActive(false);
            Map2.SetActive(true);
        }
    }
}
