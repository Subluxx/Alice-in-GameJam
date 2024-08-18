using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] otherRooms;
    private GameObject[] placeHolder;
    [SerializeField] private float scaleFactor;
    private int count = -1;
    void Awake(){
        otherRooms = new GameObject[5];
        placeHolder = GameObject.FindGameObjectsWithTag("Room");
        foreach(GameObject i in placeHolder){
            if(i.transform.position != transform.position){
                count++;
                otherRooms.SetValue(value: i, index: count);
                //otherRooms[count] = i;
            }
        }
    }
    public void enlarge(){
        if(transform.localScale.y == 1){
            transform.localScale = new Vector3(transform.localScale.x*scaleFactor, transform.localScale.y*scaleFactor,1);
            foreach(GameObject i in otherRooms){
                i.transform.localScale = new Vector3(i.transform.localScale.x/scaleFactor, i.transform.localScale.y/scaleFactor,1);
            }
        }
    }
    public void returnSize(){
        if(transform.localScale.y != 1){
            transform.localScale = new Vector3(transform.localScale.x/scaleFactor,transform.localScale.y/scaleFactor,1);
            foreach(GameObject i in otherRooms){
                    i.transform.localScale = new Vector3(i.transform.localScale.x*scaleFactor, i.transform.localScale.y*scaleFactor,1);
            }
        }
    }
}
