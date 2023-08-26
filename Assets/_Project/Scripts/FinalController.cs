using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalController : MonoBehaviour
{
    [SerializeField]
    private Transform _finalPoint;

    public Transform GetFinalPoint()
    {
        Debug.Log("GIRDI");
        return _finalPoint;
    }
}
