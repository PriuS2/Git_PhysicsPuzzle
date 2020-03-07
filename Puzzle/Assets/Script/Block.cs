using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Vector2 _initialPosition;
    
    private void Start()
    {
        _initialPosition = transform.position;
    }
}
