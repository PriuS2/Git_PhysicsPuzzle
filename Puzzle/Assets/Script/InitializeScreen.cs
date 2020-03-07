using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class InitializeScreen : MonoBehaviour
{
    public int targetFrameRate;
    private int[] _resolution;
    private PixelPerfectCamera _pixelPerfectCamera;
    private void Start()
    {
        _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        _resolution = new int[] {_pixelPerfectCamera.refResolutionX, _pixelPerfectCamera.refResolutionY};

        if (targetFrameRate == 0) targetFrameRate = 60;
        Application.targetFrameRate = targetFrameRate;
        Screen.SetResolution(_resolution[0],_resolution[1], true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
