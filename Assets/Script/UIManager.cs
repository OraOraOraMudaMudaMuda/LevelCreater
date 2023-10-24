using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
