using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private FixedJoint2D _fixedJoint2D;
    private Pin _attachedPin;
    public Pin[] pins;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        var temp = transform;
        _initialPosition = temp.position;
        _initialRotation = temp.rotation.eulerAngles;
    }


    void Start()
    {
        if (GetComponent<FixedJoint2D>())
        {
            _fixedJoint2D = GetComponent<FixedJoint2D>();
            _fixedJoint2D.enabled = false;
        }
    }

    public void ClickBlock()
    {
        switch (PuzzleSystem.Current.GetMod())
        {
            case PuzzleSystem.PuzzleMod.Hand:
                Unfreeze();
                break;
            case PuzzleSystem.PuzzleMod.Pin:
                break;
            case PuzzleSystem.PuzzleMod.Eraser:
                break;
        }
    }


    public void ResetBlock()
    {
        DetachPin();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        var quaternion = Quaternion.Euler(_initialRotation);
        transform.SetPositionAndRotation(_initialPosition, quaternion);
    }


    public void AttachPin(Rigidbody2D rb2d, Pin pin)
    {
        _fixedJoint2D.enabled = true;
        _fixedJoint2D.connectedBody = rb2d;
        _attachedPin = pin;
    }

    public void DetachPin()
    {
        _fixedJoint2D.enabled = false;
        _attachedPin.DetachPin();
        _attachedPin = null;
    }
    
    
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Block>())
            other.gameObject.GetComponent<Block>().Unfreeze();
    }


    public void Unfreeze()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        //_rigidbody2D.mass
    }
}
