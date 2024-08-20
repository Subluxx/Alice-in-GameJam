using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public GameObject GlassObject;
    public GameObject WaterGlassObject;
    public GameObject SinkObject;
    public GameObject NecklaceObject;
    public GameObject LampObject;
    public GameObject ChairObject;
    public GameObject TableObject;
    public GameObject BedObject;
    public GameObject PianoObject;
    public GameObject CouchObject;
    public GameObject VaseObject;
    public GameObject ShardObject;
    public GameObject MarblesObject;
    public GameObject KeyObject;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlassSink()
    {
        Debug.Log("Fill with water");
        GlassObject.SetActive(false);
        WaterGlassObject.SetActive(true);
    }

    public void WaterGlassFire()
    {
        Debug.Log("Extinguish fire");
    }

    public void NecklaceNPC()
    {
        Debug.Log("Given necklace to npc");
    }

    public void LampBathroom()
    {
        Debug.Log("lit up the bathroom");
    }

    public void CharacterPiano()
    {
        Debug.Log("played the piano");
    }

    public void MarblesVase()
    {
        Debug.Log("put marbles in vase");
    }

    public void VaseExplode()
    {
        Debug.Log("vase exploded");
    }

    public void ShardCouch()
    {
        Debug.Log("ripped the couch");
    }

    public void KeyDoor()
    {
        Debug.Log("opened the door");
    }


}
