using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManager : MonoBehaviour
{
    public Transform tubeAnchor;
    public Rigidbody2D tubeRB;

    private Rigidbody2D PlayerRB;
    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Calculate the extrapolated target position of the tail anchor.
        Vector2 targetPosition = tubeAnchor.position;
        targetPosition += PlayerRB.velocity * Time.fixedDeltaTime;

        tubeRB.MovePosition(targetPosition);
        tubeRB.SetRotation(tubeAnchor.rotation);
    }
}
