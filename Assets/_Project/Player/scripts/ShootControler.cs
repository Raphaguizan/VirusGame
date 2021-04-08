using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootControler : MonoBehaviour
{
    public static ShootControler Instance;
    private void Awake()
    {
        Instance = this;
        StartCoroutine(MachineGun());
    }

    public GameObject bullet;

    private float timeBtShoot;
    private float shootSpeed;

    private bool IsShotting = false;
    
    public void MachineGunCtrl(bool state, float time = 0.2f, float speed = 20f)
    {
        IsShotting = state;
        if (!state)return;

        timeBtShoot = time;
        shootSpeed = speed;

    }
    private IEnumerator MachineGun()
    {
        while (true)
        {
            if (IsShotting)
            {
                Fire(shootSpeed);
                yield return new WaitForSeconds(timeBtShoot);
            }
            yield return null;
        }
    }
    private void Fire(float speed = 20f)
    {
        GameObject aux = Instantiate(bullet, transform.position, transform.rotation);
        aux.GetComponent<Bullet>().Configure(LevelManager.GetAntibodySelected(), speed);
    }
}
