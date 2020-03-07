using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalRotate : MonoBehaviour
{
    private Transform _imageTransform;
    public float rotateSpeed;

    private void Start()
    {
        _imageTransform = transform.GetChild(0);
    }

    private void FixedUpdate()
    {
        _imageTransform.Rotate(Vector3.back*rotateSpeed);
    }
}
