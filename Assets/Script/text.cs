using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class text : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("1111");
        print("������ײ");
    }
}
