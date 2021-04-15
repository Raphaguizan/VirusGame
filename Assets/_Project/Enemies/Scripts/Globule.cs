using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globule : Enemy
{
    Transform target;
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
    }
}
