using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemPickup : Interactable
{
    public Item item;
    public PuzzleScript puzzleScript;

    string objName;
    string obj2Name;
    

    void Start()
    {

        string objName = "";
        string obj2Name = "";
    }

    public override void Interact()
    {
        Debug.Log("Picking up?");
        base.Interact();

        PickUp();

    }

    void PickUp()
    {


        Debug.Log("Pick up" + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Debug.Log("yupie");
            gameObject.SetActive(false);
            /*Destroy(gameObject);*/
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        obj2Name = "";
        objName = "";
        if (collision.gameObject.tag == "Interactable")
        {
            ItemPickup collidedObject = collision.gameObject.GetComponent<ItemPickup>();

            obj2Name = collidedObject.item.name;
            objName = item.name;

            CheckInteraction(objName, obj2Name);

            
        }
    }

    

    public void CheckInteraction(string obj, string obj2)
    {
        
        switch (obj, obj2)
        {
            case ("Necklace", "Grandma"):
                Debug.Log(obj + " + " + obj2);
                puzzleScript.NecklaceNPC();
                break;
            

            case ("Glass", "Fireplace"):
                Debug.Log(obj + " + " + obj2);
                puzzleScript.NecklaceNPC();
                break;
            

            default:
                Debug.Log("No known interactions");
                break;
        }
    }
}
