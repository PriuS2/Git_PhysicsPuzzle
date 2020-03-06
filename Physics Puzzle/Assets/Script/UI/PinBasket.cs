using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBasket : MonoBehaviour
{
    public Transform temporaryPin;
    public Vector3 offset;
    private Pin _overlapedPin;
    private Transform _pinGraphic;
    
    private void Start()
    {
        _pinGraphic = temporaryPin.transform.Find("Graphic");
    }

    public void ResetTemporaryPos()
    {
        temporaryPin.position = Vector3.back*20;
    }

    public void UpdateTemporaryPos(Vector3 touchPosition)
    {
        temporaryPin.position = touchPosition + offset;
        if (_overlapedPin)
        {
            _pinGraphic.position = _overlapedPin.transform.position + Vector3.back*3;
        }
        else
        {
            _pinGraphic.position = temporaryPin.position;
        }
    }

    public void TryAttachPin()
    {
        ResetTemporaryPos();
        if (_overlapedPin)
        {
            _overlapedPin.AttachPin();
            
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _overlapedPin = other.GetComponent<Pin>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _overlapedPin = null;
    }
}
