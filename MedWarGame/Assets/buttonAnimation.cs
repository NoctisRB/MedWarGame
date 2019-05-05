using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonAnimation : MonoBehaviour
{
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool up;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        up = false;
    }
}
