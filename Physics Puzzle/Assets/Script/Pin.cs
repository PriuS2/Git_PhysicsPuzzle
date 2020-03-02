using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void AttachPin()
    {
        transform.parent.GetComponent<Block>().AttachPin(_rigidbody2D, this);
    }

    public void DetachPin()
    {
        
    }

}
