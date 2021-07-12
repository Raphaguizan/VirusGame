using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EyeMove : MonoBehaviour
{
    public Transform eyeBall, eyeRotate, target;
    private static EyeMove Instance;

    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        eyeRotate.right = new Vector2(target.position.x, target.position.y) - new Vector2(eyeRotate.position.x, eyeRotate.position.y);
        eyeBall.rotation = Quaternion.identity;
    }

    public static void SetTarget(Transform newTarget)
    {
        Instance.target = newTarget;
    }
}
