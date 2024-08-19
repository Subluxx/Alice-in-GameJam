using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{
    public GameObject GlassObject;
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GlassFire()
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
