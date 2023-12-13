using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ItemFinder
{
    
    public static Item FindInNPCInventory(InventoryNPC inven, Image image)
    {
        foreach(Item ite in inven.ItemsInInventory)
        {

            if (image.sprite.name.Length > 5)
            {
                if (ite.itemName == image.sprite.name.Substring(6))
                {
                    return ite;
                }
                else
                {
                    return null;
                }
            }

        }
        return null;
    }

}
