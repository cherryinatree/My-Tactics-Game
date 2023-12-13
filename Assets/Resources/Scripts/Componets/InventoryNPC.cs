using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryNPC
{ 
     //GameData data;
    MainSaveData saveFile;
    private List<Item> itemsInInventory;




    public InventoryNPC()
    {
        itemsInInventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemsInInventory.Add(item); 
    }
    public void RemoveItem(Item item)
    {
        itemsInInventory.Remove(item); 
    }
    public List<Item> RetrieveItems()
    {
        return itemsInInventory;
    }
    public void ReplaceAllItems(List<Item> newItems)
    {
        itemsInInventory = newItems;
    }
    public void SaveItems()
    {
       // Save1.SaveItems(itemsInInventory);
    }
    public void LoadItems()
    {

    }

    public List<Item> ItemsInInventory
    {
        get { return itemsInInventory; }
        set { itemsInInventory = value;  }
    }
}
