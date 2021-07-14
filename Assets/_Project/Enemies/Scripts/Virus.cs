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

    protected override void ChangeColor(Color c)
    {
        SpriteRenderer[] parts = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < parts.Length; i++)
        {
            if (!parts[i].gameObject.name.Equals("eyes"))
            {
                Debug.Log(parts[i].gameObject.name +" color = "+c);
                parts[i].color = c;
                Debug.Log(parts[i].gameObject.name + " NOW = "+ parts[i].color);
            }
        }
    }
}
