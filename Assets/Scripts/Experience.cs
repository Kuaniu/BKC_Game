using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float xp=0;
    public string ExpName;
    private ExpBallPool expballPool;//经验球对象池


    private void ReturnExpBallPool(string ExpName)//回收
    {
        expballPool.ReturnObjectToPool(ExpName, gameObject);
    }
}
