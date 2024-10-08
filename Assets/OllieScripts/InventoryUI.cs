using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    public GameObject inventoryUI;  
    public Transform itemsParent;   

    Inventory inventory;    // Our current inventory

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
    }

    
    void Update()
    {
        /*if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateUI();
        }*/
        
        UpdateUI();
    }

   
    public void UpdateUI()
    {
        InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

}