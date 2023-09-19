using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    private float BullMoveSpeed;
    //private Vector3 target;
    void Start()
    {
        BullMoveSpeed = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.RotateAround(transform.parent.position, Vector3.forward, BullMoveSpeed);
    }
}
