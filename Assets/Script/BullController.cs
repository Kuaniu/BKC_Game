using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //���������˺�
    //public float BullDamage = 1;
    //������ת�ٶ�
    private float BullMoveSpeed = 50;
    private void FixedUpdate()
    {
        //����Χ�������ת
        transform.RotateAround(transform.parent.position, Vector3.forward, BullMoveSpeed);

    }
}
