using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float xp=0;
    public string ExpName;
    private ExpBallPool expballPool;//����������


    private void ReturnExpBallPool(string ExpName)//����
    {
        expballPool.ReturnObjectToPool(ExpName, gameObject);
    }
}
