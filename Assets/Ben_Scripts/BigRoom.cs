using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BigRoom : MonoBehaviour
{
    // Start is called before the first frame update\
    public GameObject[] otherRooms;
    private GameObject[] placeHolder;
    private GameObject wall;
    private GameObject currentRoom;
    private float scaleFactor = 1.5f;
    private int count = -1;
    private float changeInX;
    private float changeInY;
    // private float changeInX2;
    // private float changeInY2;
    private Vector3 size;
    private bool changeSize = true;
    Collider collider;
    void Awake(){
        otherRooms = new GameObject[6];
        placeHolder = GameObject.FindGameObjectsWithTag("Room");
        wall = GameObject.FindGameObjectWithTag("Wall");
        collider = GetComponent<Collider>();
        size = collider.bounds.size;
        //Debug.Log(size);
        // changeInX = (150-100)/2;
        // changeInY = (100-100/1.5f)/2;
        foreach(GameObject i in placeHolder){
            if(i.transform.position != transform.position && count<5){
                //i.transform.localScale = new Vector3(100,100,100);
                count++;
                //Debug.Log(count);
                otherRooms.SetValue(value: i, index: count);
                //otherRooms[count] = i;
            }else{
                currentRoom = i;
            }
        }
    }
    public void enlarge(){
        if(changeSize == true){
            //Debug.Log(transform.localScale);
            //house.transform.position = reset;
            wall.SetActive(false);
            if(currentRoom.name == "bathroom" || currentRoom.name == "recreation"){
                transform.localScale = new Vector3(100, transform.localScale.y*scaleFactor,100);
            }else{
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y*scaleFactor,transform.localScale.z*scaleFactor);
            }
            // if(transform.position == GameObject.Find("kitchen").transform.position){
            //     transform.position = new Vector3(transform.position.x+5, transform.position.y-2, transform.position.z);
            // }else if(transform.position == GameObject.Find("dinningroom").transform.position){
            //     transform.position = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
            // }else if(transform.position == GameObject.Find("bedroom").transform.position){
            //     transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            // }else if(transform.position == GameObject.Find("recreation").transform.position){
            //     transform.position = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
            // }
            changeInX = (size.x*1.5f-size.x)/2;
            changeInY = (size.y*1.5f-size.y)/2;
            reOrginise(currentRoom.name,0);
            //Debug.Log(changeInX);
            // foreach(GameObject i in otherRooms){
            //     i.transform.localScale = new Vector3(100, i.transform.localScale.y/scaleFactor,i.transform.localScale.z/scaleFactor);
            //     changeInX2 = ((i.GetComponent<Collider>().bounds.size.x-i.GetComponent<Collider>().bounds.size.x/1.5f)/2);
            //     changeInY2 = ((i.GetComponent<Collider>().bounds.size.y-i.GetComponent<Collider>().bounds.size.y/1.5f)/2);
            //     //Debug.Log(changeInX2+" "+changeInY2);
            //     //changeInY = (i.transform.localScale.y*1.5f - i.transform.localScale.y)/2;
            //     reOrginise2(i.name,0,i,changeInX2,changeInY2);
            //     //i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
            //     //i.transform.position = new Vector3(i.transform.position.x/scaleFactor,i.transform.position.y/scaleFactor,i.transform.position.z);
            //     // if(i.transform.position == GameObject.Find("attic").transform.position){
            //     //     i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y+3, i.transform.position.z);
            //     // }
            // }
            changeSize = false;
            //house.transform.position = new Vector3(-20.7f,0,0);
        }
    }
    public void returnSize(){
        if(changeSize == false && transform.localScale.y != 100){
            wall.SetActive(true);
            if(currentRoom.name == "bathroom" || currentRoom.name == "recreation"){
                transform.localScale = new Vector3(100,transform.localScale.y/scaleFactor,100);
            }else{
                transform.localScale = new Vector3(100,transform.localScale.y/scaleFactor,transform.localScale.z/scaleFactor);
            }
            reOrginise(currentRoom.name,1);
            //transform.position = new Vector3(transform.position.x-changeInX,transform.position.y+changeInY,transform.position.z);
            // if(transform.position == GameObject.Find("kitchen").transform.position){
            //     transform.position = new Vector3(transform.position.x-5, transform.position.y+2, transform.position.z);
            // }else if(transform.position == GameObject.Find("dinningroom").transform.position){
            //     transform.position = new Vector3(transform.position.x-2, transform.position.y, transform.position.z);
            // }else if(transform.position == GameObject.Find("bedroom").transform.position){
            //     transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
            // }else if(transform.position == GameObject.Find("recreation").transform.position){
            //     transform.position = new Vector3(transform.position.x+2, transform.position.y, transform.position.z);
            // }
            // foreach(GameObject i in otherRooms){
            //     i.transform.localScale = new Vector3(100, i.transform.localScale.y*scaleFactor,i.transform.localScale.z*scaleFactor);
            //     changeInX2 = ((i.GetComponent<Collider>().bounds.size.x*1.5f-i.GetComponent<Collider>().bounds.size.x)/2);
            //     changeInY2 = ((i.GetComponent<Collider>().bounds.size.y*1.5f-i.GetComponent<Collider>().bounds.size.y)/2);
            //     //Debug.Log((i.GetComponent<Collider>().bounds.size.x*1.5f)/2);
            //     reOrginise2(i.name,1,i,changeInX2,changeInY2);
            //     //i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
            //     // if(i.transform.position == GameObject.Find("attic").transform.position){
            //     //     i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y-3, i.transform.position.z);
            //     // }
            // }
            changeSize = true;
        }
    }
    private void reOrginise(String room, int type){
        switch(room){
            case "kitchen":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y-changeInY,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y+changeInY,transform.position.z);
                }
                break;
            case "recreation":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y,transform.position.z); 
                }
                break;
            case "entrance":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y-changeInY,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y+changeInY,transform.position.z);
                }
                break;
            case "dinningroom":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y+changeInY,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y-changeInY,transform.position.z);
                }
                break;
            case "bathroom":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y,transform.position.z);
                }
                break;
            case "balcony":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y,transform.position.z);
                }
                break;
            case "attic":
                if(type == 0){
                    transform.position = new Vector3(transform.position.x-changeInX,transform.position.y,transform.position.z);
                }else{
                    transform.position = new Vector3(transform.position.x+changeInX,transform.position.y,transform.position.z);
                }
                break;
        }
    }
    // private void reOrginise2(String room, int type, GameObject i, float changeInX2, float changeInY2){
    //     switch(room){
    //         case "kitchen":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "recreation":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z); 
    //             }
    //             break;
    //         case "entrance":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "dinningroom":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "bedroom":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "bathroom":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "balcony":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }
    //             break;
    //         case "attic":
    //             if(type == 0){
    //                 i.transform.position = new Vector3(i.transform.position.x+changeInX2,i.transform.position.y-changeInY2,i.transform.position.z);
    //             }else{
    //                 i.transform.position = new Vector3(i.transform.position.x-changeInX2,i.transform.position.y+changeInY2,i.transform.position.z);
    //             }
    //             break;
    //     }
    // }
}
