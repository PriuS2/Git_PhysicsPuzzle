using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool _isAttached;
    private BoxCollider2D _collider2D;
    public GameObject pinCollider;

    private void Start()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        _isAttached = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        //StartCoroutine(Test());
    }


    IEnumerator Test()
    {
        yield return new WaitForSeconds(.2f);
        AttachPin();
    }


    public void AttachPin()
    {
        

        if (_isAttached) return;
        
        Debug.Log("attach");

        transform.parent.GetComponent<Block>().AttachPin(_rigidbody2D, this);
        _isAttached = true;
        pinCollider.SetActive(true);
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _collider2D.enabled = false;
    }

    public void DetachPin()
    {
        _isAttached = false;
        pinCollider.SetActive(false);
        _collider2D.enabled = true;
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
    }
}
