using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    #region iniciaçização
    private Rigidbody2D rb;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        Physics2D.IgnoreLayerCollision(7, 8, false);
        direction = Vector2.zero;

        shield.SetActive(false);
        ChangePowerColor();

        StartCoroutine(MovimentUpdate()); 
        StartCoroutine(EyeTrackingCtrl());

        backGroundLigth.color = Color.white;

        ShotControler.Initialize(anim);// inicializa o controle de arma passando o animator
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
    private bool isShooting = false;
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
        {
            isShooting = true;
            anim.SetLayerWeight(1, 1);
            ShotControler.Instance.WeaponCtrl(true);
        }
        else if (ctx.phase == InputActionPhase.Canceled)
        {
            isShooting = false;
            StartCoroutine(ShootAnimationBackToPosition());
            ShotControler.Instance.WeaponCtrl(false);
        } 
    }
    // muda gradualmente a animação de tiro para a idle
    IEnumerator ShootAnimationBackToPosition()
    {
        float shootCtrl = 1;
        while (shootCtrl > 0 && !isShooting)
        {
            shootCtrl -= Time.deltaTime;
            anim.SetLayerWeight(1, shootCtrl);
            yield return null;
        }
    }
    #endregion

    #region escudo
    public Light2D backGroundLigth;
    private void OnEnable()
    {
        PowerUpController.ShieldChange += ShieldCtrl;
    }
    private void OnDisable()
    {
        PowerUpController.ShieldChange -= ShieldCtrl;
    }

    [SerializeField]
    private GameObject shield;
    private bool shieldActive = false;
    [Tooltip("The speed of the power rotation")]
    public float powerSpeed = 1f;

    private void ShieldCtrl()
    {
        if (PowerUpController.isShieldActive)
        {
            this.shield.SetActive(true);
            shieldActive = true;
            ChangePowerColor();
            this.shield.GetComponent<Animator>().speed = powerSpeed;
        }
        else
        {
            shield.SetActive(false);
            shieldActive = false;
            backGroundLigth.color = Color.white;
        }
        
    }

    void ChangePowerColor()
    {
        if(shieldActive) backGroundLigth.color = LevelManager.GetAntibodySelected().color;
        foreach (Transform s in shield.transform)
        {
            SpriteRenderer color = s.GetComponent<SpriteRenderer>();
            Light2D light = s.GetComponent<Light2D>();
            if (s.gameObject.activeInHierarchy)
            {
                color.color = LevelManager.GetAntibodySelected().color;
                light.color = LevelManager.GetAntibodySelected().color;
            }
        }
    }
    
    
    #endregion

    #region colisão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //colisão com virus vivo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // verifica se o shield está ativo e na cor certa para evitar o dano
            Enemy aux = collision.gameObject.GetComponent<Enemy>();
            if (!shieldActive || aux.colorType != LevelManager.GetAntibodySelected().colorType)
            {
                DamageTaked();
            }
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
            if (!shieldActive)
                DamageTaked();
        }

        //colisão com powerUp
        if (collision.gameObject.CompareTag("powerUp"))
        {
            PowerUpInstance aux = collision.gameObject.GetComponent<PowerUpInstance>();
            PowerUpController.Instace.ActivatePowerUp(aux.type, aux.duration);
            collision.gameObject.SetActive(false);
        }
    }
    #endregion

    #region dano
    [Tooltip("time to player stay invunerable")]
    public float invunerableTime;
    // ativa funções quando o player toma dano
    void DamageTaked()
    {
        
        Physics2D.IgnoreLayerCollision(7, 8);// desliga a colisão com inimigos
        ChangeColor(Color.black); // muda a cor(ou ativa animação)
        StartCoroutine(InvunerableTime(invunerableTime));// chama a corrotina que ativa tudo novamente
        LevelManager.RemoveAntiBody();// chama a função de remover anticorpo
    }

    // tempo ivunerável do player
    IEnumerator InvunerableTime(float time)
    {
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        ChangeColor(Color.white);
    }

    private void ChangeColor(Color c)
    {
        SpriteRenderer[] renderes = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sR in renderes)
        {
            sR.color = c;
        }
    }
    #endregion
    #region eyeTracking
    public EyeMove EyeMoveScript;
    public Transform defaultEyeTarget;

    IEnumerator EyeTrackingCtrl()
    {
        Collider2D[] hitColliders = new Collider2D[10];
        while (true)
        {
            // pega todos os objetos das layers Enemy e Power em um determinado range (5f)
            int numColliders = Physics2D.OverlapCircleNonAlloc(this.transform.position, 5f, hitColliders, LayerMask.GetMask("Enemy", "Power"));

            if (numColliders == 0 || isShooting) EyeMoveScript.SetTarget(defaultEyeTarget);
            else
            {
                EyeMoveScript.SetTarget(LookForCloser(hitColliders, numColliders));
            }
            yield return new WaitForFixedUpdate();
        } 
    }

    private Transform LookForCloser(Collider2D[] hits, int num)
    {
        float minDistance = 100f;
        Transform resp = null;
        if (num > 10) num = 10;
        for (int i = 0; i < num; i++)
        {
            float dist = Vector2.Distance(hits[i].transform.position, transform.position);
            if (dist < minDistance)
            {
                resp = hits[i].transform;
                minDistance = dist;
            }
        }
        return resp;
    }
    #endregion
}
