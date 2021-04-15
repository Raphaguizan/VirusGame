using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    public List<SpawnActions> spawns;
}

[Serializable]
public class SpawnActions
{
    public SpawnObject obj;
    public int index;
}

[Serializable]
public class SpawnObject
{
    [Tooltip("game object to spawn")]
    public GameObject gameObject;
    [Tooltip("Color of the enemy (if hematia or globule put NONE)")]
    public ColorType color;
    [Tooltip("spawn speed")]
    public float speed;
}
