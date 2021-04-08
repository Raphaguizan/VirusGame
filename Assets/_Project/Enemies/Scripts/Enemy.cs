using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ColorType colorType;
    public int life = 1;

    public void Initialize(ColorType t)
    {
        GetComponent<SpriteRenderer>().color = LevelManager.GetColorOfType(t);
        colorType = t;
    }

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
}
