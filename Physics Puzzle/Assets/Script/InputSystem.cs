using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputSystem : MonoBehaviour
{
    [Tooltip("if check show touch point")] public bool showTouchPoint;

    private Camera _mainCam;

    private Vector3 _touchPositon;
    private PinBasket _pinBasket;
    
#if UNITY_EDITOR
    private int _screenWidth;
    private int _screenHeight;
#endif

    void Start()
    {
        _mainCam = Camera.main;
        _pinBasket = PuzzleSystem.Current.pinBasket;
        

#if UNITY_EDITOR
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
#endif
    }

    void Update()
    {
//__________Unity Editor__________
#if UNITY_EDITOR
        if (showTouchPoint)
        {
            var mousePositionDebug = Input.mousePosition;
            var worldPosDebug = _mainCam.ScreenToWorldPoint(mousePositionDebug);
            var start = new Vector3(worldPosDebug.x - 0.3f, worldPosDebug.y, -.2f);
            Debug.DrawLine(start, start + Vector3.right * 0.6f, Color.magenta, 0.016f, true);
            start = new Vector3(worldPosDebug.x, worldPosDebug.y - 0.3f, -.2f);
            Debug.DrawLine(start, start + Vector3.up * 0.6f, Color.magenta, 0.016f, true);
        }

        var mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            
            BeganTouch(mousePosition);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndedTouch(mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            MovedTouch(mousePosition);
        }

#endif

//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            _touchPositon = _mainCam.ScreenToWorldPoint(touch.position);
            Debug.Log(_touchPositon);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("Began Touch");
                    BeganTouch(_touchPositon);
                    break;
                case TouchPhase.Moved:
                    //Debug.Log("Moved Touch");
                    MovedTouch(_touchPositon);
                    break;
                case TouchPhase.Stationary:
                    //Debug.Log("Stationary Touch");
                    StationaryTouch(_touchPositon);
                    break;
                case TouchPhase.Ended:
                    //Debug.Log("Ended Touch");
                    EndedTouch(_touchPositon);
                    break;
            }
        }
#endif
    }


    //touch Fx
    private void BeganTouch(Vector3 touchPosition)
    {

        var worldPos = _mainCam.ScreenToWorldPoint(touchPosition);
        var touchPos = new Vector2(worldPos.x, worldPos.y);
        var ray = new Ray2D(touchPos, Vector2.zero);
        var rayHit = Physics2D.Raycast(ray.origin, ray.direction);
        if (rayHit)
        {
            Debug.Log("InputSystem / rayHit.name : "+rayHit.transform.name);

            var hitTransform = rayHit.transform;
            if (hitTransform.CompareTag("Block"))
            {
                var block = hitTransform.GetComponent<Block>();
                block.ClickBlock();
            }
            else if (hitTransform.CompareTag("UI"))
            {
                var ui = hitTransform.GetComponent<PuzzleInterface>().content;
                if (ui == PuzzleInterface.UIContent.Pin)
                {
                    PuzzleSystem.Current.Mod = PuzzleSystem.PuzzleMod.Pin;
                }
                else if (ui == PuzzleInterface.UIContent.Eraser)
                {
                    PuzzleSystem.Current.Mod = PuzzleSystem.PuzzleMod.Eraser;
                }
                else if (ui == PuzzleInterface.UIContent.Undo)
                {
                }
                else if (ui == PuzzleInterface.UIContent.Reset)
                {
                }
                else if (ui == PuzzleInterface.UIContent.Back)
                {
                }
                else
                {
                    PuzzleSystem.Current.Mod = PuzzleSystem.PuzzleMod.Hand;
                }
            }
            else if (hitTransform.CompareTag("Pin"))
            {
                hitTransform.GetComponent<Pin>().AttachPin();
            }
        }
    }

    private void MovedTouch(Vector3 touchPosition)
    {
        if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Hand) return;

        var worldPos = _mainCam.ScreenToWorldPoint(touchPosition);
        var touchPos = new Vector2(worldPos.x, worldPos.y);
        
        if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Pin)
        {
            _pinBasket.UpdateTemporaryPos(touchPos);
        }
        else if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Eraser)
        {
            
        }
        
    }

    private void StationaryTouch(Vector3 touchPosition)
    {
        //if (_currentMod == PuzzleSystem.PuzzleMod.Hand) return;
    }

    private void EndedTouch(Vector3 touchPosition)
    {
        if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Hand) return;
        
        var worldPos = _mainCam.ScreenToWorldPoint(touchPosition);
        var touchPos = new Vector2(worldPos.x, worldPos.y);
        
        if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Pin)
        {
            _pinBasket.TryAttachPin();
        }
        else if (PuzzleSystem.Current.Mod == PuzzleSystem.PuzzleMod.Eraser)
        {
            //
        }
        PuzzleSystem.Current.Mod = PuzzleSystem.PuzzleMod.Hand;
    }
}