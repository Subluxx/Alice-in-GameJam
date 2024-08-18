using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSizeObject : MonoBehaviour
{
    private float cool = 1;
    public void reSizeObject(){
        cool += Time.deltaTime;
        transform.localScale = new Vector3(cool, cool, 1);
        
    }
    public void shrinkObject(){
        cool -= Time.deltaTime;
        transform.localScale = new Vector3(cool, cool, 1);
    }
}
