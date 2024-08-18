using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

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
}
