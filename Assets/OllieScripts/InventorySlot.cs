using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour {

	public Image icon;
	public Button removeButton;

	Item item;
	public GameObject itemPickup;



    public void AddItem (Item newItem)
	{
		item = newItem;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	public void ClearSlot ()
	{
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	public void RemoveItemFromInventory ()
	{
		SpawnItem();
		

	   Debug.Log("!!!");
		Inventory.instance.Remove(item);
	}


    public void SpawnItem()
    {
        itemPickup.SetActive(true);
        //spawn a 3d item back to the scene
    }

    // Use the item
    public void UseItem ()
	{
		if (item != null)
		{
			item.Use();
		}
	}

}