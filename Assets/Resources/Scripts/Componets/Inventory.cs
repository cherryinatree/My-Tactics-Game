using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Inventory
{
    //GameData data;
    MainSaveData saveFile;
    private List<Item> itemsInInventory;



    public Inventory()
    {
        itemsInInventory = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemsInInventory.Add(item);// ItemSlots.OnInventoryItemChange();
    }
    public void RemoveItem(Item item)
    {
        itemsInInventory.Remove(item);// ItemSlots.OnInventoryItemChange();
    }
    public List<Item> RetrieveItems()
    {
        return itemsInInventory;
    }
    public void ReplaceAllItems(List<Item> newItems)
    {
        itemsInInventory = newItems; //ItemSlots.OnInventoryItemChange();
    }
    public void SaveItems()
    {
        //Save1.SaveItems(itemsInInventory);
    }
    public void LoadItems()
    {

    }

    public List<Item> ItemsInInventory
    {
        get { return itemsInInventory; }
        set
        {
            itemsInInventory = value;//ItemSlots.OnInventoryItemChange(); }
        }
    }
}
