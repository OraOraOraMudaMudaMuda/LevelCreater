using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera Instance { get; private set; }
    public Transform followTarget;

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Update()
    {
        if(followTarget != null)
            MoveCamera(followTarget);
    }
    public void MoveCamera(Transform _target)
    {
        transform.position = _target.transform.position - (Vector3.forward * 100f);
    }
}
