using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    private void FixedUpdate()
    {
        Follow();
    }
    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, -5), 10*Time.deltaTime);
    }
}
