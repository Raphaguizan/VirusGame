using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region initialize
    public ColorType colorType;
    public int life = 1;

    protected Vector2 direction;
    protected Animator anim;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeColor(LevelManager.GetColorOfType(colorType));
        direction = Vector2.left;
        anim = GetComponentInChildren<Animator>();
    }

    public void Initialize(ColorType t, float speed = 3f)
    {
        ChangeColor(LevelManager.GetColorOfType(t));
        colorType = t;
        this.speed = speed;
    }

    protected virtual void ChangeColor(Color c)
    {
        // Virus color change
    }
    #endregion

    #region moviment
    public float speed = 3f;// velocidade de movimento
    protected Rigidbody2D rb;

    protected virtual void FixedUpdate()
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
                StartCoroutine(DamageAnimation(0.05f));
                DamageBehaviour();
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

    #region damage
    protected virtual void DamageBehaviour()
    {
        //Do Something
    }
    IEnumerator DamageAnimation(float time)
    {
        anim.SetLayerWeight(1, 1);
        yield return new WaitForSeconds(time);
        anim.SetLayerWeight(1, 0);
    }
    #endregion
}
