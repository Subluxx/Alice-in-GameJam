using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] otherRooms;
    void Awake(){
        otherRooms = GameObject.FindGameObjectsWithTag("Room");
    }
    public void enlarge(){
        // foreach(Transform childTransfrom in this.transform){
        //     Transform w = childTransfrom.GetComponent<Transform>();
        //     w.localScale = new Vector3(w.localScale.x*2, w.localScale.y*2,1);
        // }
        transform.localScale = new Vector3(transform.localScale.x*1.5f, transform.localScale.y*1.5f,1);
        foreach(GameObject i in otherRooms){
            i.transform.localScale = new Vector3(i.transform.localScale.x/1.5f, i.transform.localScale.y/1.5f,1);
        }
        //otherRoom.transform.localScale = new Vector3(otherRoom.transform.localScale.x/1.5f, otherRoom.transform.localScale.y/1.5f,1);
    }
    // void OnTriggerEnter(){
    //     transform.position = new Vector3(transform.position.x+0.5f,transform.position.y,1);
    // }
}
