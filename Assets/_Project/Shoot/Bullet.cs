using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ColorType colorType;
    public float speed;

    public void Configure(AntiBody ab, float speed)
    {
        colorType = ab.colorType;
        GetComponent<SpriteRenderer>().color = ab.color;
        this.speed = speed * -1;
    }
    private void Update()
    {
        transform.Translate(Vector2.down.normalized * speed * Time.deltaTime);
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
