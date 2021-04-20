using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region inicia�iza��o
    private Rigidbody2D rb;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        Physics2D.IgnoreLayerCollision(7, 8, false);
        direction = Vector2.zero;

        shield.SetActive(false);
        ChangePowerColor();

        StartCoroutine(MovimentUpdate());
    }
    #endregion

    #region movimenta��o
    [Tooltip("The speed of the player")]
    public float speed;
    private Vector2 direction;
    public void Moviment(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    // f�sica de movimenta��o do personagem
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
            ChangePowerColor();
        }
    }
    public void PowerMinus(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            LevelManager.ChangeAntibody(-1);
            ChangePowerColor();
        }
    }
    #endregion

    #region tiro
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
            ShotControler.Instance.WeaponCtrl(true);
        else if (ctx.phase == InputActionPhase.Canceled)
            ShotControler.Instance.WeaponCtrl(false);
    }
    #endregion

    #region escudo
    private void OnEnable()
    {
        PowerUpController.PowerUpChange += ShieldCtrl;
    }
    private void OnDisable()
    {
        PowerUpController.PowerUpChange -= ShieldCtrl;
    }

    [SerializeField]
    private GameObject shield;
    [Tooltip("The speed of the power rotation")]
    public float powerSpeed = 1f;

    private void ShieldCtrl()
    {
        if (PowerUpController.PowerUpActive != PowerUpType.SHIELD)
            shield.SetActive(false);
        else
            shield.SetActive(true);
            shield.GetComponent<Animator>().speed = powerSpeed;
    }

    void ChangePowerColor()
    {
        SpriteRenderer [] colors = shield.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer s in colors)
        {
            if(s.enabled == true)
            {
                s.color = LevelManager.GetAntibodySelected().color;
            }
        }
    }
    
    
    #endregion

    #region colis�o
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colis�o com virus vivo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // verifica se o shield est� ativo e na cor certa para evitar o dano
            Enemy aux = collision.gameObject.GetComponent<Enemy>();
            if (PowerUpController.PowerUpActive != PowerUpType.SHIELD 
                || aux.colorType != LevelManager.GetAntibodySelected().colorType)
            {
                DamageTaked();
            }
            Destroy(collision.gameObject);
        }

        //colis�o com virus morto
        if (collision.gameObject.CompareTag("Dead_Enemy"))
        {
            Destroy(collision.gameObject);
            LevelManager.AddAntiBody();
        }

        //colis�o com hem�cia
        if (collision.gameObject.CompareTag("Hemacia"))
        {
            if (PowerUpController.PowerUpActive != PowerUpType.SHIELD)
                DamageTaked();
        }

        //colis�o com powerUp
        if (collision.gameObject.CompareTag("powerUp"))
        {
            PowerUpInstance aux = collision.gameObject.GetComponent<PowerUpInstance>();
            PowerUpController.Instace.ActivatePowerUp(aux.type, aux.duration);
            Destroy(collision.gameObject);
        }
    }
    #endregion

    #region dano
    [Tooltip("time to player stay invunerable")]
    public float invunerableTime;
    // ativa fun��es quando o player toma dano
    void DamageTaked()
    {
        
        Physics2D.IgnoreLayerCollision(7, 8);// desliga a colis�o com inimigos
        this.GetComponent<SpriteRenderer>().color = Color.black; // muda a cor(ou ativa anima��o)
        StartCoroutine(InvunerableTime(invunerableTime));// chama a corrotina que ativa tudo novamente
        LevelManager.RemoveAntiBody();// chama a fun��o de remover anticorpo
    }

    // tempo ivuner�vel do player
    IEnumerator InvunerableTime(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    #endregion
}
