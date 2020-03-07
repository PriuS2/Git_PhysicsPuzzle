using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _isAttached;
    public bool IsAttached => _isAttached;

    private BoxCollider2D _collider2D;
    public GameObject pinCollider;

    private int _attachedNum;
    public int AttachedNum
    {
        get => _attachedNum;
        set => _attachedNum = value;
    }

    private bool _isBlockUnfreezed;
    public bool SetUnfreeze
    {
        set => _isBlockUnfreezed = value;
    }

    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        _isAttached = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void AttachPin()
    {
//        Debug.Log("?????");
        if (_isAttached)
        {
            DetachPin();
            return;
        }

        if (_isBlockUnfreezed) return;
        
        
        Debug.Log("attach");

        transform.parent.GetComponent<Block>().AttachPin(_rigidbody2D, this);
        _isAttached = true;
        pinCollider.SetActive(true);
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _collider2D.enabled = false;
    }

    public void UpdateJoint()
    {
        transform.parent.GetComponent<Block>().AttachPin(_rigidbody2D, this);
        _isAttached = true;
        pinCollider.SetActive(true);
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _collider2D.enabled = false;
    }

    public void DetachPin()
    {
        Debug.Log("Pin / DetachPin()");
        _isAttached = false;
        pinCollider.SetActive(false);
        _collider2D.enabled = true;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        transform.parent.GetComponent<Block>().DetachPin(this);
    }
}
