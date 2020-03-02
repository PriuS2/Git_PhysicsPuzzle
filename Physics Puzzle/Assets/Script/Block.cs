using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private FixedJoint2D _fixedJoint2D;
    private Pin _attachedPin;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _fixedJoint2D = GetComponent<FixedJoint2D>();
        _fixedJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
    
    
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Block>())
            other.gameObject.GetComponent<Block>().Unfreeze();
    }


    public void Unfreeze()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }
}
