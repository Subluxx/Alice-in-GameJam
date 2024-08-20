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

    public GameObject MarbleObject;
    public GameObject MarbleObject1;
    public GameObject MarbleObject2;
    public GameObject MarbleObject3;
    public GameObject MarbleObject4;

    public GameObject KeyObject;


    public GameObject FirstWall;
    public GameObject SecondWall;
    public GameObject ThirdWall;

    public ParticleSystem FireParticles;

    public int numParticles = 0;


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
        //disable fire particles
        NecklaceObject.SetActive(true);
    }

    public void NecklaceNPC()
    {
        Debug.Log("Given necklace to npc");
        NecklaceObject.SetActive(false);
        //disable invisible wall

    }

    public void LampBathroom()
    {
        //if lamp object is big enough
        Debug.Log("lit up the bathroom");
        //enable bathroom light

    }

    public void CharacterPiano()
    {
        //if character is beneath the piano and big enough
        Debug.Log("played the piano");
        //disable invisible wall
    }

    public void MarblesVase()
    {
        Debug.Log("put marbles in vase");
        numParticles++;
        MarbleObject.SetActive(false);
    }

    public void VaseExplode()
    {
        if(numParticles > 3)
        {
            Debug.Log("vase exploded");
            VaseObject.SetActive(false);
            ShardObject.SetActive(true);
        }
        
    }

    public void ShardCouch()
    {
        Debug.Log("ripped the couch");
        ShardObject.SetActive(false);
        KeyObject.SetActive(true);
    }

    public void KeyDoor()
    {
        Debug.Log("opened the door");
        KeyObject.SetActive(false);
        //disable invisible wall
    }


}
