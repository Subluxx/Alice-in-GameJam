using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public GameObject[] background;
    int index;
    public int pages = 3;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(index >= pages)
        {
            index = pages;
            //switch to next scene
            Debug.Log("End game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        else
        {
            if (index < 0)
            {
                index = 0;
            }

            if (index == 0)
            {
                background[0].gameObject.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Next panel");

                Next();
            }
        }

        
    }

    public void Next()
    {
        index++;

        if (index < pages)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].gameObject.SetActive(false);
                background[index].gameObject.SetActive(true);
            }
        }
        
    }

    
}
