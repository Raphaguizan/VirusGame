using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region initialize
    public ColorType colorType;
    public int life = 1;

    protected Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.left;
    }

    public void Initialize(ColorType t, float speed = 3f)
    {
        GetComponent<SpriteRenderer>().color = LevelManager.GetColorOfType(t);
        colorType = t;
        this.speed = speed;
    }
    #endregion

    #region moviment
    public float speed;// velocidade de movimento
    protected Rigidbody2D rb;

    protected void FixedUpdate()
    {
        // física para movimentar o objeto
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
    }
    #endregion

    #region colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Shoot"))
        {
            ColorType type = collision.gameObject.GetComponent<Bullet>().colorType;
            Destroy(collision.gameObject);
            if (colorType == ColorType.NONE || type == colorType)
            {
                life--;
                if (life <= 0) Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // se destroi quando bate no collider "Destroy"
        if (other.gameObject.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }
    #endregion
}
