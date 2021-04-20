using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globule : Enemy
{
    Transform target;
    private float lerpDirection = 0;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    protected override void FixedUpdate()
    {
        if (target.position.x > transform.position.x + 1.1f)lerpDirection += Time.fixedDeltaTime;
        else lerpDirection -= Time.fixedDeltaTime; ;

        lerpDirection = Mathf.Clamp(lerpDirection, 0, 1);

        direction = Vector2.Lerp(target.position - transform.position, Vector2.left, lerpDirection);
        transform.right = direction * -1;

        base.FixedUpdate();
    }
}
