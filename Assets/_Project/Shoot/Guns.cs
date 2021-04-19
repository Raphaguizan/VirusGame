using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="nova arma", menuName = "Game/Gun")]
public class Guns : ScriptableObject
{
    public  float BulletSpeed = 20f;
    public  float BulletCooldown = 0.2f;
    public  int NumberOfShots = 1;
    public  PowerUpType Type = PowerUpType.NONE;
}
