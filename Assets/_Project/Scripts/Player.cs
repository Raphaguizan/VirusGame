using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region iniciaçização
    private Rigidbody2D rb;

    private GameObject power;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(7, 8, false);
        direction = Vector2.zero;
        power = this.transform.Find("Power").gameObject;
        changePowerColor();

        StartCoroutine(RotateShield());
        StartCoroutine(MovimentUpdate());
    }
    #endregion

    #region movimentação
    [Tooltip("The speed of the player")]
    public float speed;
    private Vector2 direction;
    public void Moviment(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    // física de movimentação do personagem
    IEnumerator MovimentUpdate()
    {
        while (true)
        {
            rb.MovePosition(rb.position + direction.normalized * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        } 
    }
    #endregion

    #region mudar poder

    public void PowerAdd(InputAction.CallbackContext ctx)
    {
        if(ctx.phase == InputActionPhase.Performed)
        {
            LevelManager.ChangeAntibody(1);
            changePowerColor();
        }
    }
    public void PowerMinus(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            LevelManager.ChangeAntibody(-1);
            changePowerColor();
        }
    }
    #endregion

    #region tiro
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            Debug.Log("atirouuuu");
        }
    }
    #endregion

    #region escudo
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
    [Tooltip("The speed of the power rotation")]
    public float powerSpeed;

    //mantem o visual do poder girando
    IEnumerator RotateShield()
    {
        while (true)
        {
            power.transform.Rotate(new Vector3(0, 0, powerSpeed * Time.fixedDeltaTime * -10));
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    #region colisão
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
    #endregion

    #region dano
    [Tooltip("time to player stay invunerable")]
    public float invunerableTime;
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
    #endregion
}
