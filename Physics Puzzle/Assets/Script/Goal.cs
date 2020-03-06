using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private Vector3 _goalPosition;
    public GameObject clear;
    private void Start()
    {
        _goalPosition = transform.position;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var targetPos = other.transform.position;
        var distance = Vector3.Distance(targetPos, _goalPosition);

        if (distance < 0.5f)
        {
            var vel = other.GetComponent<Target>().Velocity;
            Debug.Log(vel);
            if (vel <= 0.01f)
            {
                PuzzleSystem.Current.StageClear();
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(Temp());//임시
            }
        }
    }

    //임시
    IEnumerator Temp()
    {
        yield return new WaitForSeconds(1);
        clear.SetActive(true);
    }
}
