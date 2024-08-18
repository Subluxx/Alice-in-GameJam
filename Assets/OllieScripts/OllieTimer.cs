using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;


    int previous_hour = 0;
    public float timeSpeed = 1f;

   

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime * timeSpeed;
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        


        if (seconds >= 60)
        {
            //give player a hint
            seconds = 0;
        }



        timerText.text = string.Format("{0:00}", seconds);

    }
}
