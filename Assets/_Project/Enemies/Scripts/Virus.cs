using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Enemy
{
    [Range(-10f, 10f)]
    public float waveHeigth = 3f;
    private float time = 0;

    protected override void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        float y = waveHeigth * Mathf.Sin(time*speed); ;
        direction = new Vector2(-speed, y);

        rb.MovePosition(rb.position + direction * Time.fixedDeltaTime);
    }
}
