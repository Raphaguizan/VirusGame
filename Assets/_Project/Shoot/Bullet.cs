using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Bullet : MonoBehaviour
{
    public ColorType colorType;
    public float speed;

    private Rigidbody2D rb;

    public void Configure(AntiBody ab, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        colorType = ab.colorType;
        GetComponent<SpriteRenderer>().color = ab.color;
        foreach(Transform child in transform)
        {
            child.GetComponent<Light2D>().color = ab.color;
        }
        this.speed = speed;

    }
    private void FixedUpdate()
    {
        // física para movimentar o objeto sempre no sentido vertical ao qual foi spawnado
        rb.MovePosition(rb.position + new Vector2(transform.up.x, transform.up.y) * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // se destroi quando bate no collider "Destroy"
        if (other.gameObject.CompareTag("DestroyShot"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // se destroi quando bate na parede
        if (collision.gameObject.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
