using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] otherRooms;
    private GameObject[] placeHolder;
    private GameObject wall;
    private float scaleFactor = 1.5f;
    private int count = -1;
    void Awake(){
        otherRooms = new GameObject[7];
        placeHolder = GameObject.FindGameObjectsWithTag("Room");
        wall = GameObject.FindGameObjectWithTag("Wall");
        foreach(GameObject i in placeHolder){
            if(i.transform.position != transform.position && count<6){
                //i.transform.localScale = new Vector3(100,100,100);
                count++;
                //Debug.Log(count);
                otherRooms.SetValue(value: i, index: count);
                //otherRooms[count] = i;
            }
        }
    }
    public void enlarge(){
        if(transform.localScale.y == 100){
            //Debug.Log(transform.localScale);
            wall.SetActive(false);
            transform.localScale = new Vector3(100, transform.localScale.y*scaleFactor,transform.localScale.z*scaleFactor);
            if(transform.position == GameObject.Find("kitchen").transform.position){
                transform.position = new Vector3(transform.position.x+5, transform.position.y-2, transform.position.z);
            }else if(transform.position == GameObject.Find("dinningroom").transform.position){
                transform.position = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
            }else if(transform.position == GameObject.Find("bedroom").transform.position){
                transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            }else if(transform.position == GameObject.Find("recreation").transform.position){
                transform.position = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
            }
            foreach(GameObject i in otherRooms){
                i.transform.localScale = new Vector3(100, i.transform.localScale.y/scaleFactor,i.transform.localScale.z/scaleFactor);
                if(i.transform.position == GameObject.Find("attic").transform.position){
                    i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y+3, i.transform.position.z);
                }
            }
        }
    }
    public void returnSize(){
        if(transform.localScale.y != 100){
            wall.SetActive(true);
            transform.localScale = new Vector3(100,transform.localScale.y/scaleFactor,transform.localScale.z/scaleFactor);
            if(transform.position == GameObject.Find("kitchen").transform.position){
                transform.position = new Vector3(transform.position.x-5, transform.position.y+2, transform.position.z);
            }else if(transform.position == GameObject.Find("dinningroom").transform.position){
                transform.position = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
            }else if(transform.position == GameObject.Find("bedroom").transform.position){
                transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            }else if(transform.position == GameObject.Find("recreation").transform.position){
                transform.position = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
            }
            foreach(GameObject i in otherRooms){
                    i.transform.localScale = new Vector3(100, i.transform.localScale.y*scaleFactor,i.transform.localScale.z*scaleFactor);
                    if(i.transform.position == GameObject.Find("attic").transform.position){
                        i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y-3, i.transform.position.z);
                    }
            }
        }
    }
}
