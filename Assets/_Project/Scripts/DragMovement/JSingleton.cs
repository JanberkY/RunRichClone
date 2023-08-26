using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    public virtual void Awake()
    {
        Instance = FindObjectOfType<T>();
    }
}
