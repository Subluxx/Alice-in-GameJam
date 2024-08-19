using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReSizeObject : MonoBehaviour
{
    private Vector3 startSize;
    public float maxSize = 3f;

    [SerializeField] private Vector3 endSize;
    [SerializeField] private float duration;
    private float elapsedTime;
    void Awake(){
        startSize = transform.localScale;
        elapsedTime = transform.localScale.x;
    }
    public void reSizeObject(){
        // while(elapsedTime <= duration){
        //     elapsedTime += Time.deltaTime;
        //     //Debug.Log(elapsedTime);
        //     float percentageComplete = elapsedTime/duration;
        //     Debug.Log(percentageComplete);
        //     transform.localScale = Vector3.Lerp(startSize, endSize, percentageComplete);

        // }
        if(transform.localScale.x< maxSize)
        {
            elapsedTime += Time.deltaTime;
            transform.localScale = new Vector3(elapsedTime, elapsedTime, elapsedTime);
        }
        
    }
    public void shrinkObject(){
        if(transform.localScale.x>1f){
            elapsedTime -= Time.deltaTime;
            transform.localScale = new Vector3(elapsedTime, elapsedTime, elapsedTime);
        }
    }

}
