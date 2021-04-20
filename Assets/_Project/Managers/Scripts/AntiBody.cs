using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "novo anticorpr", menuName = "Game/Antibody")]
public class AntiBody : ScriptableObject
{
   public string abName;
   public Color color;
   public ColorType colorType;
}
