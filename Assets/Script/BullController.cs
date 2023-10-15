using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BullController : MonoBehaviour
{
    //public float BullDamage = 1;
    private float BullMoveSpeed = 10;
    //private Vector3 target;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.forward,10f);
        transform.RotateAround(transform.parent.position, Vector3.forward, BullMoveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bull"))
        {
            Destroy(gameObject);
        }
    }
}
