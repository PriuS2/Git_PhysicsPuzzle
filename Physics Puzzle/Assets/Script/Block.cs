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

    [SerializeField] private List<Transform> attachedPins;

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
        attachedPins = new List<Transform>();
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
            if (attachedPins[i] == pin.transform)
            {
                isAttachedPin = true;
            }
        }
        
        if(!pin.IsAttached) attachedPins.Add(pin.transform);
//        pin.AttachedNum = count;
        
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
//        for (int i = 0; i < count; i++)
//        {
//            if (attachedPins[i].transform == pin.transform)
//            {
//                Debug.Log("Block / DetachPin() "+pin.transform.name);
//                attachedPins.Remove(pin);
//            }
//        }
        attachedPins.Remove(pin.transform);
        Debug.Log("fdsafdasfdas" +_isUnfreeze);
        if(_isUnfreeze) Unfreeze();

        count = attachedPins.Count;
        if (count == 0)
        {
            _fixedJoint2D.enabled = false;
        }
        else if(count == 1)
        {
            attachedPins[0].GetComponent<Pin>().UpdateJoint();
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
//        if (!_isUnfreeze)
//        {
//            
//        }
        Unfreeze();
        _isUnfreeze = true;
    }


    private void Unfreeze()
    {
        if (attachedPins.Count <= 1)
        {
            Debug.Log("Fewqfewfdsvxc");
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.drag = 0;
            _rigidbody2D.angularDrag = 0.1f;
        }
        for (int i = 0; i < pins.Length; i++)
        {
            pins[i].SetUnfreeze = true;
        }
    }
}