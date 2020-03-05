using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PuzzleSystem : MonoBehaviour
{
    public static PuzzleSystem Current;
    
    public enum PuzzleMod
    {
        Hand,
        Pin,
        Eraser
    }

    [SerializeField]private PuzzleMod mod;

    private void Awake()
    {
        Current = this;
        mod = PuzzleMod.Hand;
    }

    public void SelectHand()
    {
        mod = PuzzleMod.Hand;
    }
    
    public void SelectPin()
    {
        mod = PuzzleMod.Pin;
    }

    public void SelectEraser()
    {
        mod = PuzzleMod.Eraser;
    }
    
    public void UnDo()
    {
        
    }

    public void ResetLevel()
    {
        
    }

    
    
    
    public PuzzleMod GetMod()
    {
        return mod;
    }

    public void StageClear()
    {
        Debug.Log("StageClear");
    }
}
