using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [Header("����ֵ")]
    public float xp=0;
    private void Start()
    {
        //60���δ��ʰȡ��ɾ������
        Invoke("DestroyGameObject", 60);

    }

    private void DestroyGameObject()//ɾ������
    {
        Destroy(gameObject);
    }
}
