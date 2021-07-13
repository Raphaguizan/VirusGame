using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EyeMove : MonoBehaviour
{
    public Transform eyeBall, eyeRotate, target;
    public bool IsPlayerTarget;

    private void Start()
    {
        if (IsPlayerTarget)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // mantem o olho virado para o alvo
    void Update()
    {
        eyeRotate.right = new Vector2(target.position.x, target.position.y) - new Vector2(eyeRotate.position.x, eyeRotate.position.y);
        eyeBall.right = transform.right;
    }

    /// <summary>
    /// muda o alvo que o olho deve seguir
    /// </summary>
    /// <param name="newTarget"></param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
