using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleInterface : MonoBehaviour
{
    public enum UIContent
    {
        Pin,
        Eraser,
        Undo,
        Reset,
        Back
    }

    public UIContent content;
}
