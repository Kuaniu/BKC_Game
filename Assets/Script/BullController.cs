using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //ÎäÆ÷¹¥»÷ÉËº¦
    //public float BullDamage = 1;
    //ÎäÆ÷Ğı×ªËÙ¶È
    private float BullMoveSpeed = 50;
    private void FixedUpdate()
    {
        //ÎäÆ÷Î§ÈÆÍæ¼ÒĞı×ª
        transform.RotateAround(transform.parent.position, Vector3.forward, BullMoveSpeed);

    }
}
