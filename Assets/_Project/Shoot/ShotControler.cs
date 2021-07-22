using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class ShotControler : MonoBehaviour
{
    public static ShotControler Instance;
    private Animator playerAnim;
    private Guns gunType = null;

    private void OnEnable()
    {
        PowerUpController.PowerUpChange += PowerUpCtrl;
    }
    private void OnDisable()
    {
        PowerUpController.PowerUpChange -= PowerUpCtrl;
    }

    [SerializeField]
    private Guns[] allGuns;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        StartCoroutine(MachineGun());// inicia a corrotina de tiro
    }

    public static void Initialize(Animator anim)
    {
        Instance.playerAnim = anim;
    }

    // recebe a ação de mudança de arma e ajusta as variáveis
    private void PowerUpCtrl()
    {
        if (PowerUpController.PowerUpActive == PowerUpType.SHIELD) return;
        Guns auxGun = GetGunByType(PowerUpController.PowerUpActive);
        timeBtShot = auxGun.BulletCooldown;
        shotSpeed = auxGun.BulletSpeed;
        shotCount = auxGun.NumberOfShots;

        gunType = auxGun;
    }

    public GameObject bullet;

    private float timeBtShot;
    private float shotSpeed;
    private int shotCount = 1;

    private bool IsShotting = false;
    
    // recebe o input do player e muda a variável IsShotting
    public void WeaponCtrl(bool state)
    {
        IsShotting = state;
    }

    // determina a quantidade de tiro e faz a contagem de tempo entre eles
    private IEnumerator MachineGun()
    {
        while (true)
        {
            if (IsShotting)
            {
                //ajusta a animação do tiro
                if (gunType.Type == PowerUpType.MACHINEGUN) playerAnim.SetBool("machineGun", true);
                else
                {
                    playerAnim.SetBool("machineGun", false);
                    playerAnim.SetTrigger("shoot");
                }

                for (int i = 0; i < shotCount; i++)
                {
                    Fire(transform.GetChild(i),shotSpeed);
                }
                yield return new WaitForSeconds(timeBtShot);
            }
            else
            {
                if (playerAnim)
                {
                    playerAnim.SetBool("machineGun", false);
                }
            }
            yield return null;
        }
    }

    // instancia e configura o tiro
    private void Fire(Transform child, float speed = 20f)
    {
        GameObject aux = Instantiate(bullet, child.position, child.rotation);
        aux.GetComponent<Bullet>().Configure(LevelManager.GetAntibodySelected(), speed);
    }

    // retorna a arma correta para o tipo de powerUp
    private Guns GetGunByType(PowerUpType p)
    {
        foreach (Guns gun in allGuns)
        {
            if(gun.Type == p)
            {
                return gun;
            }
        }
        return null;
    }
}
