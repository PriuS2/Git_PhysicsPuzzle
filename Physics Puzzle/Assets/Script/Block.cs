using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Block : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float Velocity => _rigidbody2D.velocity.magnitude;
    private FixedJoint2D _fixedJoint2D;
    public Pin[] pins;

    [SerializeField] private List<Pin> attachedPins;

    //public Pin[] pins;
    private Vector3 _initialPosition;
    private Vector3 _initialRotation;
    private bool _isUnfreeze;


    private void Awake()
    {
        //_attachedPin = new Pin[pins.Length];
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        var temp = transform;
        _initialPosition = temp.position;
        _initialRotation = temp.rotation.eulerAngles;
        //_attachedPin = new pin[]
    }


    private void Start()
    {
        _isUnfreeze = false;
        attachedPins = new List<Pin>();
        if (!GetComponent<FixedJoint2D>()) return;
        _fixedJoint2D = GetComponent<FixedJoint2D>();
        _fixedJoint2D.enabled = false;
    }

    public void ClickBlock()
    {
        switch (PuzzleSystem.Current.Mod)
        {
            case PuzzleSystem.PuzzleMod.Hand:
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _isUnfreeze = true;
                break;
            case PuzzleSystem.PuzzleMod.Pin:
                break;
            case PuzzleSystem.PuzzleMod.Eraser:
                break;
        }
    }


    public void ResetBlock()
    {
        //DetachPin();
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        var quaternion = Quaternion.Euler(_initialRotation);
        transform.SetPositionAndRotation(_initialPosition, quaternion);
        _isUnfreeze = false;
    }


    public bool AttachPin(Rigidbody2D rb2d, Pin pin)
    {
        if (_isUnfreeze) return _isUnfreeze;


        var count = attachedPins.Count;
//        if (count == 1)
//        {
//            attachedPin[0].StopRotate();
//        }


        var isAttachedPin = false;
        for (int i = 0; i < count; i++)
        {
            if (attachedPins[i] == pin)
            {
                isAttachedPin = true;
            }
        }

        attachedPins.Add(pin);
        if (!isAttachedPin)
        {
            _fixedJoint2D.enabled = true;
            _fixedJoint2D.connectedBody = rb2d;
        }

        if (count >= 1)
        {
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        return false;
    }

    public void DetachPin(Pin pin)
    {
        var count = attachedPins.Count;
        for (int i = 0; i < attachedPins.Count; i++)
        {
            if (attachedPins[i] == pin)
            {
                attachedPins.Remove(pin);
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
//        if(other.gameObject.GetComponent<Block>())
//            other.gameObject.GetComponent<Block>().Unfreeze();
        //Unfreeze();
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.drag = 0;
        _rigidbody2D.angularDrag = 0.5f;
        _isUnfreeze = true;
    }


//    public void Unfreeze()
//    {
//        if (_isUnfreeze) return;
//
//        if (attachedPins.Count <= 1)
//        {
//            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
//            _isUnfreeze = true;
//            _rigidbody2D.drag = 0;
//            _rigidbody2D.angularDrag = 0.1f;
//        }
//    }
}