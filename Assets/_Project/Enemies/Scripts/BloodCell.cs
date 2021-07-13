using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCell : Enemy
{
    public float force;// força que será aplicada
    protected override void Start()
    {
        base.Start();
        rb.AddTorque(Random.Range(-force * 2, force * 2));
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void DamageBehaviour()
    {
        anim.SetTrigger("damage");
    }
}
