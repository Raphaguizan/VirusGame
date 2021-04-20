using UnityEngine;

[CreateAssetMenu(fileName = "lista de inimigos", menuName = "Enemies/List")]
public class ListOfEnemies : ScriptableObject
{
    public GameObject[] Enemies;
}
