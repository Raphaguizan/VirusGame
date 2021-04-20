using System.Collections;
using System;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public static PowerUpType PowerUpActive;
    public static Action PowerUpChange;// ação que avisa a mudança de powerUp
    public static PowerUpController Instace;

    private void Awake()
    {
        Instace = this;
    }
    private void Start()
    {
        PowerUpActive = PowerUpType.NONE;
        PowerUpChange?.Invoke();// invoca a mudança logo no início para selecionar a arma default
    }

    // seleciona o powerUp coletado e ativa um timer com o tempo limite
    public void ActivatePowerUp(PowerUpType pUT, float duration)
    {
        PowerUpActive = pUT;
        PowerUpChange?.Invoke();
        StartCoroutine(PowerUpTime(duration));
    }

    // faz a contagem de tempo para terminar o powerUp
    IEnumerator PowerUpTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        PowerUpActive = PowerUpType.NONE;
        PowerUpChange?.Invoke();
    }
    // desativa a rotina caso a cena termine
    private void OnDisable()
    {
        StopCoroutine(PowerUpTime(0));
    }
}
