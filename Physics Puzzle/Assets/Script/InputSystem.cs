using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [Tooltip("if check show touch point")] public bool showTouchPoint;

    private Camera mainCam;

    private Vector3 TouchPositon;

#if UNITY_EDITOR
    private int _screenWidth;
    private int _screenHeight;
#endif

    void Start()
    {
        mainCam = Camera.main;

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
            var worldPosDebug = mainCam.ScreenToWorldPoint(mousePositionDebug);
            var start = new Vector3(worldPosDebug.x - 0.3f, worldPosDebug.y, -.2f);
            Debug.DrawLine(start, start + Vector3.right * 0.6f, Color.magenta, 0.016f, true);
            start = new Vector3(worldPosDebug.x, worldPosDebug.y - 0.3f, -.2f);
            Debug.DrawLine(start, start + Vector3.up * 0.6f, Color.magenta, 0.016f, true);
        }


        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;
            var worldPos = mainCam.ScreenToWorldPoint(mousePosition);
            var touchPos = new Vector2(worldPos.x, worldPos.y);
            var ray = new Ray2D(touchPos, Vector2.zero);
            var rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider)
            {
                if (rayHit.collider.gameObject.GetComponent<Block>())
                {
                    var block = rayHit.collider.gameObject.GetComponent<Block>();
                    block.ClickBlock();
                }
            }
        }

#endif

//__________Android__________
#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            TouchPositon = mainCam.ScreenToWorldPoint(touch.position);
            Debug.Log(TouchPositon);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //Debug.Log("Began Touch");
                    BeganTouch(TouchPositon);
                    break;
                case TouchPhase.Moved:
                    //Debug.Log("Moved Touch");
                    MovedTouch(TouchPositon);
                    break;
                case TouchPhase.Stationary:
                    //Debug.Log("Stationary Touch");
                    StationaryTouch(TouchPositon);
                    break;
                case TouchPhase.Ended:
                    //Debug.Log("Ended Touch");
                    EndedTouch(TouchPositon);
                    break;
            }
        }
#endif
    }


    //touch Fx
    private void BeganTouch(Vector3 touchPosition)
    {
        //_cursor.position = new Vector3(touchPosition.x, touchPosition.y, -2);
        //colorBlockManager.Attach(_cursor);
    }

    private void MovedTouch(Vector3 touchPosition)
    {
        //_cursor.position = new Vector3(touchPosition.x, touchPosition.y, -2);
    }

    private void StationaryTouch(Vector3 touchPosition)
    {
    }

    private void EndedTouch(Vector3 touchPosition)
    {
        //colorBlockManager.Detach(_cursor);
    }
}