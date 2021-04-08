using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ColorType colorType;

    public void Configure(AntiBody ab, float speed)
    {
        colorType = ab.colorType;
        GetComponent<SpriteRenderer>().color = ab.color;
        GetComponent<MoveThings>().speed = speed * -1;
    }
}
