using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInstance : MonoBehaviour
{
    public PowerUpType type;
    public float duration = 10f;

    [SerializeField]
    private Color color = Color.white;
    [SerializeField]
    private float speed = 3f;
    private Rigidbody2D rb;

    private Vector2 direction;
    private void Start()
    {
        GetComponent<SpriteRenderer>().color = color;
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.left;
    }

    private void FixedUpdate()
    {
        // física para movimentar o objeto
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // se destroi quando bate no collider "Destroy"
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }
}
