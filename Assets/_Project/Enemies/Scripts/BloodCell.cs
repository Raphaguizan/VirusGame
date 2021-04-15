using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : Enemy
{
    public float force;// for�a que ser� aplicada
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.left;
        rb.AddTorque(Random.Range(-force * 2, force * 2));
    }
}
