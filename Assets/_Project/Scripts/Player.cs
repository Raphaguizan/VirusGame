using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("The speed of the player")]
    public float speed;
    [Tooltip("The speed of the power rotation")]
    public float powerSpeed;
    [Tooltip("time to player stay invunerable")]
    public float invunerableTime;
    private Rigidbody2D rb;

    private GameObject power;

    private Animator anim;

    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(7, 8, false);
        direction = Vector2.zero;
        power = this.transform.Find("Power").gameObject;
        changePowerColor();
    }

    // Update is called once per frame
    void Update()
    {
        // comandos de movimentação do personagem
        //////////////////////////////////////////////////////////////////////////////
        //key down
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("right", true);
            direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetBool("left", true);
            direction += Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("up", true);
            direction += Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("down", true);
            direction += Vector2.down;
        }


        //key up
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("right", false);
            direction -= Vector2.right;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("left", false);
            direction -= Vector2.left;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("up", false);
            direction -= Vector2.up;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("down", false);
            direction -= Vector2.down;
        }
        
        //////////////////////////////////////////////////////////////////////////////
        // botões para torcar de poder
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelManager.ChangeAntibody(-1);
            changePowerColor();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LevelManager.ChangeAntibody(1);
            changePowerColor();
        }

        
    }

    void changePowerColor()
    {
        SpriteRenderer [] colors = power.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in colors)
        {
            if(s.enabled == true)
            {
                s.color = LevelManager.GetAntibodySelectedColor();
            }
        }
    }

    private void FixedUpdate()
    {
        // física de movimentação do personagem
        rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);

        //mantem o visual do poder girando
        power.transform.Rotate(new Vector3(0, 0, powerSpeed * Time.fixedDeltaTime * -10));
    }

    // testa a colisão do player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colisão com virus vivo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            damageTaked();
            Destroy(collision.gameObject);
        }

        //colisão com virus morto
        if (collision.gameObject.CompareTag("Dead_Enemy"))
        {
            Destroy(collision.gameObject);
            LevelManager.AddAntiBody();
        }

        //colisão com hemácia
        if (collision.gameObject.CompareTag("Hemacia"))
        {
            damageTaked();
        }
    }

    // ativa funções quando o player toma dano
    void damageTaked()
    {
        Physics2D.IgnoreLayerCollision(7, 8);// desliga a colisão com inimigos
        this.GetComponent<SpriteRenderer>().color = Color.black; // muda a cor(ou ativa animação)
        StartCoroutine(InvunerableTime(invunerableTime));// chama a corrotina que ativa tudo novamente
        LevelManager.RemoveAntiBody();// chama a função de remover anticorpo
    }

    // tempo ivunerável do player
    IEnumerator InvunerableTime(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
