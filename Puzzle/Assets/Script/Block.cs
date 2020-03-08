using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    private Vector2 _initialPosition;
    private Quaternion _initialQuaternion;
    
    private void Start()
    {
        var temp = transform;
        _initialPosition = temp.position;
        _initialQuaternion = temp.rotation;
    }
}
