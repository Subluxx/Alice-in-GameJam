using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject otherRoom;
    public void enlarge(){
        // foreach(Transform childTransfrom in this.transform){
        //     Transform w = childTransfrom.GetComponent<Transform>();
        //     w.localScale = new Vector3(w.localScale.x*2, w.localScale.y*2,1);
        // }
        transform.localScale = new Vector3(transform.localScale.x*2, transform.localScale.y*2,1);
        otherRoom.transform.localScale = new Vector3(otherRoom.transform.localScale.x/2, otherRoom.transform.localScale.y/2,1);
    }
}
