using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //���������˺�
    public float BullDamage = 2;
    //������ת�ٶ�
    public float BullMoveSpeed = 10;
    private void FixedUpdate()
    {
        //����Χ�������ת
        transform.RotateAround(transform.parent.position, Vector3.forward, BullMoveSpeed);
    }
}
