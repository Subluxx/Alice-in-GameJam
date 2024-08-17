using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSizeRoom : MonoBehaviour
{
    private float cool = 1;
    public void ReSizeObject(){
            cool += Time.deltaTime;
            transform.localScale = new Vector3(cool, cool, 1);
        
    }
}
