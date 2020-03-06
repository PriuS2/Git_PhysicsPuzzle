using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolutionAndFrameRate : MonoBehaviour
{
    public int frameRate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = frameRate;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(360, 640, true);
    }
}
