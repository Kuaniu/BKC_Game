using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [Header("经验值")]
    public float xp=0;
    private void Start()
    {
        //60秒后未被拾取则删除自身
        Invoke("DestroyGameObject", 60);

    }

    private void DestroyGameObject()//删除自身
    {
        Destroy(gameObject);
    }
}
